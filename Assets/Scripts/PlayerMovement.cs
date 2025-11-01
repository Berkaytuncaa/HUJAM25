using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private Vector2 _input;

    [Header("Rotation Settings")]
    [SerializeField] private Camera _mainCamera;

    [Header("Melee Weapon Settings")]
    [SerializeField] private GameObject _meleeWeapon;
    [SerializeField] private float _attackDuration = 0.2f;
    [SerializeField] private float _attackCooldown = 0.5f;

    private bool _isAttacking = false;
    private float _attackTimer = 0f;

    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();

        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    void Update()
    {
        CheckInput();
        Move();
        RotateTowardsMouse();

        if (_attackTimer > 0)
            _attackTimer -= Time.deltaTime;
    }

    private void CheckInput()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        _input.Normalize();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isAttacking && _attackTimer <= 0)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    private void Move()
    {
        _rb.linearVelocity = (_input * _speed);
    }

    private bool IsMoving()
    {
        return _input.sqrMagnitude > 0.01f;
    }



    private IEnumerator MeleeAttack()
    {
        _isAttacking = true;
        _attackTimer = _attackCooldown;

        // Enable the melee weapon hitbox
        _meleeWeapon.SetActive(true);
        Debug.Log("Player swings melee weapon!");

        yield return new WaitForSeconds(_attackDuration);

        // Disable hitbox after attack ends
        _meleeWeapon.SetActive(false);
        _isAttacking = false;
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = (mousePos - transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // (subtract 90° if your sprite faces up by default)
        _rb.rotation = angle;
        // _rb.rotation = angle - 90f;
    }
}
