using UnityEngine;

public class MonsterDeath : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Transform _splodePoint;

    public void Splode()
    {
        Instantiate(_explosion, _splodePoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
