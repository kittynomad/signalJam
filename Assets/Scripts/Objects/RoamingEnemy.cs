using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoamingEnemy : MonoBehaviour, IKillable
{
    [SerializeField] private float _movespeed;
    [SerializeField] private float _timeForTurn;
    [SerializeField] private GameObject[] _moveToPoints;
    [SerializeField] private LayerMask _solidLayer;
    [SerializeField] private float _maxExposedTime;
    private Collider2D coll;
    //[SerializeField] private BackgroundHazard _bH;
    [SerializeField] private Animator[] _anim;


    private int targetPosIndex = 0;
    private float leeway = 0.1f;
    private float exposedTime;
    [SerializeField] private bool behindWall;
    public bool falling;

    public bool moveLock = false;

    private Rigidbody2D rb;

    public Action<RoamingEnemy> KilledAction;

    [SerializeField] private GameObject _corpse;

    public float ExposedTime { get => exposedTime; set => exposedTime = value; }
    public bool BehindWall { get => behindWall; set => behindWall = value; }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
        StartCoroutine(MoveCoroutine());
    }

    private bool MoveTowardsTarget()
    {
        
        Vector2 moveDirection = (rb.position - (Vector2)_moveToPoints[targetPosIndex].transform.position).normalized;
        if (IsGrounded())
        {
            if (falling)
            {
                for (int i = 0; i < _anim.Length; i++)
                {
                    _anim[i].Play("EnemyWalk");
                    falling = false;
                }
            }
            if (!moveLock)
                rb.MovePosition(rb.position + (moveDirection.x > 0 ? Vector2.left : Vector2.right) * _movespeed + (Physics2D.gravity * rb.gravityScale * Time.fixedDeltaTime));
            for (int i = 0; i < _anim.Length; i++)
            {
                if (moveDirection.x < 0)
                    _anim[i].SetBool("RightMode", true);
                else
                    _anim[i].SetBool("RightMode", false);
            }
               

        }
        else
        {
            if (!falling)
            {
                if (_anim.Length > 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        _anim[i].Play("EnemyFalling");
                        falling = true;
                        AudioManager.PlaySound("FunnyScream", 0.5f);
                    }
                }
                else
                {

                    for (int i = 0; i < _anim.Length; i++)
                    {
                        _anim[i].Play("EnemyFalling");
                        falling = true;
                        AudioManager.PlaySound("FunnyScream", 0.5f);
                    }
                }
            }
        }
        //rb.MovePosition(rb.position + Vector2.left * moveDirection * _movespeed);
        return Mathf.Abs(transform.position.x - _moveToPoints[targetPosIndex].transform.position.x) <= leeway;
    }

    private IEnumerator MoveCoroutine()
    {
        while(true)
        {
            if (MoveTowardsTarget())
            {
                if(targetPosIndex == _moveToPoints.Length - 1)
                {
                    targetPosIndex = 0;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    targetPosIndex++;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                //targetPosIndex = targetPosIndex >= _moveToPoints.Length - 1 ? 0 : targetPosIndex + 1;
                Debug.Log("turning");
                yield return new WaitForSeconds(_timeForTurn);
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            pb.OnDamage(1, gameObject);
        }
    }

    public bool OnDamage(float damageAmount = 1, GameObject damageSource = null)
    {
        OnKill(damageSource);
        return true;
    }

    public void OnKill(GameObject damageSource = null)
    {
        if(KilledAction != null)
        {
            KilledAction?.Invoke(this);
        }
        Instantiate(_corpse, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetTargetPoints(GameObject[] points)
    {
        targetPosIndex = 0;
        _moveToPoints = points;
    }

    public bool IsGrounded()
    {
        //check if grounded (duh)
        bool hg = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, _solidLayer);
        return hg;
    }

    public void Hi(GameObject g)
    {
        //_bH = g.GetComponent<BackgroundHazard>();
    }

    public void SafeWall()
    {
        //_bH.KillDog();
    }

    public void FUUUUUCK()
    {
        //_bH.SickEm(gameObject);
    }
}
