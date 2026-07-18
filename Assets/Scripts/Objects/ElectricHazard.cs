using System;
using Unity.Mathematics;
using UnityEngine;

public class ElectricHazard : MonoBehaviour
{
    [SerializeField] bool isToggledOn = false;

    [SerializeField] private GameObject[] _sparkleVFX;

    [SerializeField] private BoxCollider2D _bC2D;
    [SerializeField] private float _sparkleFreq;

    [SerializeField] private GameObject _visualON;
    [SerializeField] private GameObject _visualOFF;

    private void Start()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        if (isToggledOn)
        {
                if (UnityEngine.Random.Range(0, 100) >= _sparkleFreq)
                {
                    Sparkle();
                }
        }
    }

    public void Sparkle()
    {
        GameObject sparkle = _sparkleVFX[UnityEngine.Random.Range(0, _sparkleVFX.Length - 1)];
        Vector2 pluh = _bC2D.bounds.size;
        float x = UnityEngine.Random.Range(-pluh.x, pluh.x) / 2;
        float y = UnityEngine.Random.Range(-pluh.y, pluh.y) / 2;
        GameObject newbornSparkle = Instantiate(sparkle, transform.position, Quaternion.identity);
        newbornSparkle.transform.parent = gameObject.transform;
        newbornSparkle.transform.position = new Vector2(newbornSparkle.transform.position.x + x, newbornSparkle.transform.position.y + y);
        newbornSparkle.transform.parent = gameObject.transform;
    }

    public void ToggleActive()
    {
        isToggledOn = !isToggledOn;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        UpdateState();
    }

    private void UpdateState()
    {
        //put visual changes here?
        //im putting placeholder SLOPPP here for now
        if(isToggledOn)
        {
            _visualON.SetActive(true);
            _visualOFF.SetActive(false);
            //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            _visualON.SetActive(false);
            _visualOFF.SetActive(true);
            //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isToggledOn && collision.gameObject.TryGetComponent(out IKillable ik))
        {
            ik.OnDamage(1, gameObject);
        }
    }
}
