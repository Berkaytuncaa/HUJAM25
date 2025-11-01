using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
     [SerializeField] private Transform target; // the player
     [SerializeField] private float acceleration = 8f;
     [SerializeField] private float steering = 100f;
     [SerializeField] private float maxSpeed = 15f;
     [SerializeField] private float rayLength = 100f;          // how far ahead to check
     [SerializeField] private LayerMask obstacleMask;        // layer for walls or obstacles

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target is null) return;
        Vector2 direction = (new Vector2(target.position.x, target.position.y) - _rb.position).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayLength, obstacleMask);
        if (hit.collider)
        {
            // if we see a wall, steer away from it
            Vector2 avoidDir = Vector2.Reflect(transform.up, hit.normal);
            direction = Vector2.Lerp(direction, avoidDir, 0.7f); // blend between chasing and avoiding
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float newAngle = Mathf.LerpAngle(_rb.rotation, angle, Time.fixedDeltaTime * 5f);
        _rb.MoveRotation(newAngle);
        
        // --- 4️⃣ Move forward ---
        _rb.AddForce(transform.up * acceleration);

        // --- 5️⃣ Clamp speed ---
        if (_rb.linearVelocity.magnitude > maxSpeed)
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed; 
    }
}
