using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedEnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool faceUpwardsSprite = true;

    private Vector2 _direction;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Find the player once at spawn
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            // Calculate direction toward player's position at spawn time
            _direction = (player.position - transform.position).normalized;

            // Rotate to face player
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            if (faceUpwardsSprite)
                angle -= 90f; // Adjust if sprite faces upward
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // fallback
            _direction = transform.right;
        }

        // Give it velocity in that direction
        _rb.linearVelocity = _direction * speed;

        // Destroy after lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (!collision.isTrigger)
        {
            // Destroy if it hits environment
            Destroy(gameObject);
        }
    }
}
