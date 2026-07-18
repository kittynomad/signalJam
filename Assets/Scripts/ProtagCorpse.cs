using System;
using System.Collections;
using UnityEngine;

public class ProtagCorpse : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rB2D;
    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private float _xForce;
    [SerializeField] private float _yForce;
    [SerializeField] private float _respawnDelay;

    private Vector2 startPoint;
    private GameObject player;
  

    [SerializeField] private float _corpseFallDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerBehaviors>().gameObject;
        startPoint = transform.position;
        if (_sR.flipX)
        {
            _rB2D.linearVelocity = new Vector2(_xForce, _yForce);
        }
        else
        {
            _rB2D.linearVelocity = new Vector2(-_xForce, _yForce);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= startPoint.y - _corpseFallDistance)
        {
            StartCoroutine(DeathEnd());
        }
    }

    public IEnumerator DeathEnd()
    {
        yield return new WaitForSeconds(_respawnDelay);
        player.GetComponent<PlayerBehaviors>().Respawn();
        Destroy(gameObject);
    }

}
