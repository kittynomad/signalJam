using UnityEngine;

public class EnemyCorpse : MonoBehaviour
{
    private float _xForce;
    private float _yForce;

    private Vector2 startPoint;

    private float _corpseFallDistance = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _xForce = Random.Range(-15, 15);
        if (_xForce < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        _yForce = Random.Range(15, 20);
        startPoint = transform.position;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(_xForce, _yForce);
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= startPoint.y - _corpseFallDistance)
        {
            Destroy(gameObject);
        }
    }

}
