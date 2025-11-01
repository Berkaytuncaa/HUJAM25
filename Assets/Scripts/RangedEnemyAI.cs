using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public LayerMask playerLayer;
    private Rigidbody2D _rb;
    private PlayerMovement _playerMovement;

    [Header("Ranges")]
    public float roamRadius = 5f;
    public float agroRange = 6f;
    public float attackRange = 1.5f;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float roamWaitTime = 2f;

    private Vector2 _startPosition;
    private Vector2 _roamTarget;
    private float _waitTimer;
    private bool _isAgro;
    private bool _isAttacking;
    private bool _hasTakenDamage = false;

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f;
    private float _attackTimer = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 10f;
    public bool faceUpwardsSprite = false;

    [Header("Health")]
    [SerializeField] private float _maxHealth = 2;
    private float _currentHealth;

    [Header("Damage Feedback")]
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _hitColor = Color.red;
    [SerializeField] private float _flashDuration = 0.1f;

    [Header("Knockback & Stun")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float stunDuration = 0.25f;
    private bool _isStunned = false;

    [Header("Ranged Attack Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 8f;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();

        if (player != null)
            _playerMovement = player.GetComponent<PlayerMovement>();

        _startPosition = transform.position;
        PickNewRoamTarget();

        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (_isStunned) return;

        if (_attackTimer > 0)
            _attackTimer -= Time.deltaTime;

        bool inAgroRange = Physics2D.OverlapCircle(transform.position, agroRange, playerLayer);
        bool inAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (inAttackRange)
        {

            Vector2 dir = (player.position - transform.position).normalized;
            RotateTowards(dir);

            AttackPlayer();
        }
        else if (inAgroRange || _hasTakenDamage && !_isAttacking)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            RotateTowards(dir);

            ChasePlayer();
        }
        else
        {
            Roam();
        }
    }

    private void Roam()
    {
        _isAgro = false;
        _isAttacking = false;

        Vector2 direction = _roamTarget - (Vector2)transform.position;
        if (direction.magnitude > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _roamTarget, moveSpeed * Time.deltaTime);
            RotateTowards(direction);
        }

        // Move toward roam target
        transform.position = Vector2.MoveTowards(transform.position, _roamTarget, moveSpeed * Time.deltaTime);
        Debug.Log("I am roaming!!");
        // Reached roam point
        if (Vector2.Distance(transform.position, _roamTarget) < 0.2f)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= roamWaitTime)
            {
                PickNewRoamTarget();
                _waitTimer = 0;
            }
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("I must chase the Player!!");
        _isAgro = true;
        _isAttacking = false;

        if (player != null)
        {
            Debug.Log("I am chasing the Player!!");
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            RotateTowards(direction);
        }
    }

    private void AttackPlayer()
    {
        _isAgro = true;

        if (_attackTimer > 0)
            return;

        _isAttacking = true;

        if (player == null || projectilePrefab == null || firePoint == null)
            return;

        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Debug.Log($"{gameObject.name} shoots at player!");

        _attackTimer = attackCooldown;
    }

    private void PickNewRoamTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle * roamRadius;
        _roamTarget = _startPosition + randomDirection;
    }

    private void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (faceUpwardsSprite)
            angle -= 90f; // adjust if sprite faces up

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage, Vector2 hitSourcePosition)
    {
        _currentHealth -= damage;
        StartCoroutine(FlashRedEffect());
        ApplyKnockback(hitSourcePosition);

        if (_currentHealth <= 0)
        {
            Death();
        }
        _hasTakenDamage = true;
    }

    private IEnumerator FlashRedEffect()
    {
        if (_spriteRenderer == null)
            yield break;

        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = _hitColor;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.color = originalColor;
    }

    private void ApplyKnockback(Vector2 hitSourcePosition)
    {
        if (_rb == null) return;

        Vector2 knockDir = ((Vector2)transform.position - hitSourcePosition).normalized;

        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);

        _isStunned = true;
        StartCoroutine(RecoverFromStun());
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(stunDuration);
        _isStunned = false;
        if (_rb != null)
            _rb.linearVelocity = Vector2.zero;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the ranges for visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_startPosition == Vector2.zero ? transform.position : (Vector3)_startPosition, roamRadius);
    }
}
