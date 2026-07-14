using UnityEngine;
using System;

public class PlayerBehaviors : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private LayerMask _solidLayer;

    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private Animator _anim;

    private PlayerController pc;
    private Rigidbody2D rb;
    private Collider2D coll;

    private bool jumpHeld = false;
    private Vector2 lastSafePosition;

    public Action<PlayerBehaviors> interactAction;

    public Vector2 LastSafePosition { get => lastSafePosition; set => lastSafePosition = value; }

    private void Start()
    {
        pc = gameObject.GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
        lastSafePosition = transform.position;
    }

    private void FixedUpdate()
    {
        
        rb.linearVelocityX = pc.MovementDirection.x * _runSpeed;
        UpdateAnimator();
    }

    public void JumpBehavior()
    {
        if(IsGrounded())
        {
            _anim.Play("PlayerJumpStart");
            jumpHeld = true;
            rb.linearVelocityY = _jumpSpeed;
        }
    }

    public void EndJumpBehavior()
    {
        jumpHeld = false;
        if (rb.linearVelocityY > 0)
            rb.linearVelocityY = 0;
    }

    public bool IsGrounded()
    {
        //check if grounded (duh)
        bool hg = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, _solidLayer);
        return hg;
    }

    private void UpdateAnimator()
    {
        _anim.SetFloat("YSpeed", rb.linearVelocityY);
        _anim.SetBool("IsGrounded", IsGrounded());

        if (pc.MovementDirection.x == -1)
        {
            _sR.flipX = true;
            _anim.SetBool("IsMoving", true);
        }
        else if (pc.MovementDirection.x == 1)
        {
            _sR.flipX = false;
            _anim.SetBool("IsMoving", true);
        }
        else
        {
            _anim.SetBool("IsMoving", false);
        }
    }

    public void UpdateSafePosition()
    {
        lastSafePosition = transform.position;
    }

    public void Respawn()
    {
        transform.position = lastSafePosition;
    }

    public void InteractBehavior()
    {
        //broadcast interactAction if it has subscribers
        if (interactAction != null)
        {
            interactAction?.Invoke(this);
        }

    }
}
