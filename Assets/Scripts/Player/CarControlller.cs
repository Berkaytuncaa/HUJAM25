using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class CarControlller : MonoBehaviour
    {
        
        private Rigidbody2D _rb;
        private float _moveInput;
        private float _turnInput;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float maxSpeed = 20f;

        private float _originalDamping;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _originalDamping = _rb.linearDamping;
        }

        private void Update()
        {
            CheckInput();
            Debug.Log(_rb.linearVelocity);
        }

        private void FixedUpdate()
        {
            // 1️⃣  Accelerate the car in its forward direction
            _rb.AddForce(transform.up * _moveInput * speed);

            // 2️⃣  Limit top speed
            if (_rb.linearVelocity.magnitude > maxSpeed)
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;

            // 3️⃣  Rotate based on speed and steering
            //float direction = Mathf.Sign(Vector2.Dot(_rb.linearVelocity, transform.up)); // forward or reverse
            float direction = _moveInput >= 0 ? 1f : -1f;
            _rb.MoveRotation(_rb.rotation + _turnInput * (speed * 5)  * Time.fixedDeltaTime * direction);
            
            Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb.linearVelocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(_rb.linearVelocity, transform.right);
            _rb.linearVelocity = forwardVelocity + rightVelocity * 0.3f; // reduce side slip

        }


        private void CheckInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _moveInput = 1;
                _rb.linearDamping = 0;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _moveInput = -1;
                _rb.linearDamping = 0;
            }
            else
            {
                _moveInput = 0;
                _rb.linearDamping = _originalDamping;
            }

            _turnInput = Input.GetKey(KeyCode.A) ? 1 :
                Input.GetKey(KeyCode.D) ? -1 : 0;
        }
    }
}
