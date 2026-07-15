using UnityEngine;
using System.Collections;

public class RoamingEnemy : MonoBehaviour
{
    [SerializeField] private float _movespeed;
    [SerializeField] private float _timeForTurn;
    [SerializeField] private GameObject[] _moveToPoints;

    private int targetPosIndex = 0;
    private float leeway = 0.1f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(MoveCoroutine());
    }

    private bool MoveTowardsTarget()
    {
        Vector2 moveDirection = (rb.position - (Vector2)_moveToPoints[targetPosIndex].transform.position).normalized;
        rb.MovePosition(rb.position + moveDirection * _movespeed);

        return Mathf.Abs(rb.position.x - _moveToPoints[targetPosIndex].transform.position.x) <= leeway;
    }

    private IEnumerator MoveCoroutine()
    {
        while(true)
        {
            if (MoveTowardsTarget())
            {
                targetPosIndex = targetPosIndex >= _moveToPoints.Length - 1 ? 0 : targetPosIndex + 1;
                yield return new WaitForSeconds(_timeForTurn);
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
        }
        
    }

}
