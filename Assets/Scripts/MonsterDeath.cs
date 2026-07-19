using UnityEngine;

public class MonsterDeath : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private PlayerBehaviors _pB;
    [SerializeField] private AudioManager _aM;
    [SerializeField] private Transform _splodePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pB.ExitHorror();
        _aM.WindCutOff();
    }

    public void Splode()
    {
        Instantiate(_explosion, _splodePoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
