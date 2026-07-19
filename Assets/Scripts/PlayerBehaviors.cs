using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviors : MonoBehaviour, IKillable
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private float _maxExposedTime;

    [SerializeField] private LayerMask _solidLayer;

    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _noSignalZapVFX;
    [SerializeField] private GameObject _signalPingVFX;
    [SerializeField] private float _signalYOffset;

    [SerializeField] private AudioManager _aM;

    [SerializeField] private GameObject _corpseLeft;
    [SerializeField] private GameObject _corpseRight;
    [SerializeField] private Animator _faceAnim;
    private bool faceSleep = true;

    [SerializeField] private SpriteRenderer[] _backgroundProps;
    [SerializeField] private SpriteRenderer _scaryBackground;
    public bool EnteringHorror;
    public bool ExitingHorror;
    public bool HorrorRiser;
    private float HorrorVol;

    private PlayerController pc;
    private Rigidbody2D rb;
    private Collider2D coll;

    private bool crouchHeld = false;
    private Vector2 lastSafePosition;
    private float exposedTime;
    private bool behindWall;

    public Action<PlayerBehaviors> interactAction;

    public Vector2 LastSafePosition { get => lastSafePosition; set => lastSafePosition = value; }
    public float ExposedTime { get => exposedTime; set => exposedTime = value; }
    public bool BehindWall { get => behindWall; set => behindWall = value; }

    private void Start()
    {
        pc = gameObject.GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
        lastSafePosition = transform.position;
    }

    private void FixedUpdate()
    {
        crouchHeld = pc.MovementDirection.y < 0;
        if (!crouchHeld)
        {
            rb.linearVelocityX = pc.MovementDirection.x * _runSpeed;
        }
        else
        {
            rb.linearVelocityX = 0f;
        }
        
        UpdateAnimator();
        if (HorrorRiser && HorrorVol < 0.7f && !coll.isTrigger)
        {
            HorrorVol += 0.03f;
            _aM.ChangeHorrorVolume(HorrorVol);
        }
        else if (!HorrorRiser && HorrorVol > 0)
        {
            HorrorVol -= 0.05f;
            _aM.ChangeHorrorVolume(HorrorVol);
        }

        if (EnteringHorror)
        {
            for (int i = 0; i < _backgroundProps.Length - 1; i++)
            {
                Color col = _backgroundProps[i].color;
                col.a = _backgroundProps[i].color.a - 0.01f;
                _backgroundProps[i].color = col;
            }
            Color col2 = _scaryBackground.color;
            col2.a = _scaryBackground.color.a + 0.03f;
            _scaryBackground.color = col2;
            if (_backgroundProps[0].color.a <= 0)
            {
                EnteringHorror = false;
                col2.a = 1;
                _scaryBackground.color = col2;
            }
        }
        if (ExitingHorror)
        {
            for (int i = 0; i < _backgroundProps.Length - 1; i++)
            {
                Color col = _backgroundProps[i].color;
                col.a = _backgroundProps[i].color.a + 0.06f;
                _backgroundProps[i].color = col;
            }
            Color col2 = _scaryBackground.color;
            col2.a = _scaryBackground.color.a - 0.06f;
            _scaryBackground.color = col2;
            if (_backgroundProps[0].color.a >= 1)
            {
                ExitingHorror = false;
                //Destroy(_scaryBackground);
            }
        }
    }

    public void JumpBehavior()
    {
        if(IsGrounded())
        {
            _anim.Play("PlayerJumpStart");
            AudioManager.PlaySound("Jump");
            rb.linearVelocityY = _jumpSpeed;
        }
    }

    public void EndJumpBehavior()
    {
        if (rb.linearVelocityY > 0)
            rb.linearVelocityY = 0;
    }

    public bool IsGrounded()
    {
        //check if grounded (duh)
        bool hg = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, _solidLayer);
        if (coll.isTrigger)
        {
            hg = false;
        }
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

    public void UpdateSafePosition(Vector2 v2)
    {
        lastSafePosition = v2;
    }

    public void Respawn()
    {
        pc.MovementDirection = Vector2.zero;
        gameObject.GetComponent<PlayerInput>().ActivateInput();
        Color col = _sR.color;
        col.a = 1;
        _sR.color = col;
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.enabled = true;
        rb.linearVelocity = new Vector2(0, 0);
        transform.position = lastSafePosition;
        Instantiate(_noSignalZapVFX, transform.position, Quaternion.identity);
    }

    public void InteractBehavior()
    {
        Instantiate(_signalPingVFX, new Vector2(transform.position.x, transform.position.y + _signalYOffset), Quaternion.identity);
        AudioManager.PlaySound("SignalPing");
        //broadcast interactAction if it has subscribers
        if (interactAction != null)
        {
            interactAction?.Invoke(this);
        }

    }

    public bool OnDamage(float damageAmount = 1, GameObject damageSource = null)
    {
        //we are Killing and Killing and Killing
        OnKill(damageSource);
        return true;
    }

    public void OnKill(GameObject damageSource = null)
    {
        exposedTime = 0f;
        AudioManager.PlaySound("Death");
        HorrorRiser = false;
        gameObject.GetComponent<PlayerInput>().DeactivateInput();
        if (_sR.flipX)
        {
            Instantiate(_corpseRight, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_corpseLeft, transform.position, Quaternion.identity);
        }
        Color col = _sR.color;
        col.a = 0;
        _sR.color = col;
        rb.bodyType = RigidbodyType2D.Static;
        coll.enabled = false;
        //gameObject.transform.position = new Vector2(9999, 0);
        //Respawn();
    }


    public bool ExposedFunction()
    {
        if(!behindWall)
        {
            exposedTime += Time.deltaTime;
            if (faceSleep)
            {
                SCARY();
            }
        }
        else
        {
            exposedTime = 0f;
        }
        return exposedTime >= _maxExposedTime;
    }

    public void SCARY()
    {
        _faceAnim.SetBool("Safe", false);
        faceSleep = false;
        HorrorRiser = true;
    }
    public void safeee()
    {
        //if (rb.bodyType == RigidbodyType2D.Dynamic)
        //{
        _faceAnim.SetBool("Safe", true);
        //}
        faceSleep = true;
        HorrorRiser = false;
    }

    public void EnterHorror()
    {
        EnteringHorror = true;
    }

    public void ExitHorror()
    {
        ExitingHorror = true;
    }

}
