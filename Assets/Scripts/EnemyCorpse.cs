using UnityEngine;

public class EnemyCorpse : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rB2D;
    [SerializeField] private float _xForce;
    [SerializeField] private float _yForce;

    private Vector2 startPoint;

    [SerializeField] private float _corpseFallDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPoint = transform.position;
        _rB2D.linearVelocity = new Vector2(_xForce, _yForce);
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= startPoint.y - _corpseFallDistance)
        {
            Destroy(gameObject);
        }
    }

}
