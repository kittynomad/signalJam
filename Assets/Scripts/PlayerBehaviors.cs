using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private LayerMask _solidLayer;

    private PlayerController pc;
    private Rigidbody2D rb;
    private Collider2D coll;

    private void Start()
    {
        pc = gameObject.GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = pc.MovementDirection.x * _runSpeed;
    }

    public void JumpBehavior()
    {
        if(IsGrounded())
        {
            rb.linearVelocityY = _jumpSpeed;
        }
    }

    public bool IsGrounded()
    {
        //check if grounded (duh)
        bool hg = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, _solidLayer);
        return hg;
    }
}
