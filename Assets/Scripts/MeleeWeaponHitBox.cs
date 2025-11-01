using UnityEngine;

public class MeleeWeaponHitBox : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage, transform.position);
                Debug.Log("Hit enemy with melee!");
            }
        }
    }
}
