using Unity.VisualScripting;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private GameObject _cloud;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _minXOffset;
    [SerializeField] private float _maxXOffset;
    [SerializeField] private float _minYOffset;
    [SerializeField] private float _maxYOffset;

    [SerializeField] private GameObject player;
    
    public void SetCloudRate(float f)
    {
        _spawnRate = f;
    }

    private void FixedUpdate()
    {
        if (UnityEngine.Random.Range(0, 100) >= _spawnRate)
        {
            float f = Random.Range(0, 10);
            float x = UnityEngine.Random.Range(_minXOffset, _maxXOffset);
            float y = UnityEngine.Random.Range(_minYOffset, _maxYOffset);
            if (f > 5)
            {
                x = -x;
            }
            Instantiate(_cloud, new Vector2 (x, player.transform.position.y + y), Quaternion.identity);
        }
    }
}
