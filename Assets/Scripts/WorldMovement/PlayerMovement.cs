using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal"); //get horizontal movement (a or d)
        _movement.y = Input.GetAxisRaw("Vertical"); //get vertical movement (w or s)
        _movement = _movement.normalized;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _movement * _moveSpeed;
    }
}