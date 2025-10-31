using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public LayerMask playerLayer;

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

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f;  // seconds between attacks
    private float _attackTimer = 0f;

    void Start()
    {
        _startPosition = transform.position;
        PickNewRoamTarget();
    }

    void Update()
    {
        if (_attackTimer > 0)
            _attackTimer -= Time.deltaTime;

        bool inAgroRange = Physics2D.OverlapCircle(transform.position, agroRange, playerLayer);
        bool inAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (inAttackRange)
        {
            AttackPlayer();
        }
        else if (inAgroRange)
        {
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
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        _isAgro = true;

        if (_attackTimer > 0)
            return;

        _isAttacking = true;

        
        Debug.Log($"{gameObject.name} attacks the player!");
        // TODO: add animations, player.currentHealth--

        _attackTimer = attackCooldown;
    }

    private void PickNewRoamTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle * roamRadius;
        _roamTarget = _startPosition + randomDirection;
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
