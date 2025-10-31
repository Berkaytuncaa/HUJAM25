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
    }

    private void CheckInput()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        _input.Normalize();
    }

    private void Move()
    {
        _rb.linearVelocity = (_input * _speed);
    }

    private bool IsMoving()
    {
        return _input.sqrMagnitude > 0.01f;
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
