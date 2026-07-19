using UnityEngine;
using System;

public class EnemyRespawner : MonoBehaviour, IKillable
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _movePoints;
    [SerializeField] private GameObject _respawnPoint;

    [SerializeField] private GameObject _vfx;

    [SerializeField] private int _hitsToDestroy = 3;
    [SerializeField] private WeakFace _wF;

    private GameObject enemy;

    private int hitsLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RespawnEnemy();
        hitsLeft = _hitsToDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnEnemy(RoamingEnemy lastEnemy = null)
    {
        if(lastEnemy != null)
        {
            lastEnemy.KilledAction -= RespawnEnemy;
        }
        Instantiate(_vfx, _respawnPoint.transform.position, Quaternion.identity);
        enemy = Instantiate(_enemyPrefab, _respawnPoint.transform.position, Quaternion.identity);
        enemy.GetComponent<RoamingEnemy>().SetTargetPoints(_movePoints);
        enemy.GetComponent<RoamingEnemy>().KilledAction += RespawnEnemy;
        if (_wF != null) 
            _wF.SetTarget(enemy);
    }

    public bool OnDamage(float damageAmount = 1, GameObject damageSource = null)
    {
        Debug.Log("generator hit");
        hitsLeft--;
        if(hitsLeft <= 0)
        {
            OnKill(damageSource);
            return true;
        }
        return false;
    }

    public void OnKill(GameObject damageSource = null)
    {
        Destroy(enemy);
        Destroy(gameObject);
    }
}
