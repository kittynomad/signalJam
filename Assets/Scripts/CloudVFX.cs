using UnityEngine;

public class CloudVFX : MonoBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Rigidbody2D _rB2D;
    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private float _appearSpeed;
    [SerializeField] private Sprite[] _cloudSprites;

    private bool spawning;
    private bool dying;
    private float maxOpacity = 0.5f;

    private float xSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.position.y >= 290)
        {
            Destroy(gameObject);
        }
        spawning = true;
        dying = false;
        xSpeed = Random.Range(_minSpeed, _maxSpeed);
        if (transform.localPosition.x < 0)
        {
            xSpeed = -xSpeed;
        }
        _rB2D.linearVelocity = new Vector2(-xSpeed, 0);
        _sR.sprite = _cloudSprites[Random.Range(0, _cloudSprites.Length - 1)];
        _sR.sortingOrder = Random.Range(-3, 3);
        if (_sR.sortingOrder >= 0)
        {
            maxOpacity = 0.2f;
        }
    }

    private void FixedUpdate()
    {
        if (spawning)
        {
            Color col = _sR.color;
            col.a = _sR.color.a + _appearSpeed;
            _sR.color = col;
            if(col.a >= maxOpacity)
            {
                spawning = false;
            }
        }
        else if (dying)
        {
            Color col = _sR.color;
            col.a = _sR.color.a - _appearSpeed;
            _sR.color = col;
            if (col.a <= 0.01)
            {
                Destroy(gameObject);
            }
        }
        else if (transform.position.x > 100 || transform.position.x < -100)
        {
            dying = true;
        }
    }
}
