using UnityEngine;

public class MeleeWeaponHitBox : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            RangedEnemyAI rangedEnemy = collision.GetComponent<RangedEnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage, transform.position);
                Debug.Log("Hit enemy with melee!");
            }
        }

        if (collision.CompareTag("RangedEnemy"))
        {
            RangedEnemyAI rangedEnemy = collision.GetComponent<RangedEnemyAI>();
            if (rangedEnemy != null)
            {
                rangedEnemy.TakeDamage(_damage, transform.position);
                Debug.Log("Hit enemy with melee!");
            }
        }
    }
}
