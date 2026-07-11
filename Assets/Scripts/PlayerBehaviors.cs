using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;

    private PlayerController pc;
    private Rigidbody2D rb;

    private void Start()
    {
        pc = gameObject.GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = pc.MovementDirection.x * _runSpeed;
    }
}
