using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private Vector2 _input;

    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        _input.Normalize();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = (_input * _speed);
    }
}
