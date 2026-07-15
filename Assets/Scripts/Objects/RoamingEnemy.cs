using UnityEngine;
using System.Collections;
using System;

public class RoamingEnemy : MonoBehaviour, IKillable
{
    [SerializeField] private float _movespeed;
    [SerializeField] private float _timeForTurn;
    [SerializeField] private GameObject[] _moveToPoints;

    private int targetPosIndex = 0;
    private float leeway = 0.1f;

    private Rigidbody2D rb;

    public Action<RoamingEnemy> KilledAction;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(MoveCoroutine());
    }

    private bool MoveTowardsTarget()
    {
        Vector2 moveDirection = (rb.position - (Vector2)_moveToPoints[targetPosIndex].transform.position).normalized;
        rb.MovePosition(rb.position + Vector2.left * moveDirection * _movespeed);
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
                }
                else
                {
                    targetPosIndex++;
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
        
        Destroy(gameObject);
    }

    public void SetTargetPoints(GameObject[] points)
    {
        targetPosIndex = 0;
        _moveToPoints = points;
    }
}
