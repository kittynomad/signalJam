using UnityEngine;
using System;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _movePoints;
    [SerializeField] private GameObject _respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RespawnEnemy();
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
        GameObject enemy = Instantiate(_enemyPrefab, _respawnPoint.transform.position, Quaternion.identity);
        enemy.GetComponent<RoamingEnemy>().SetTargetPoints(_movePoints);
        enemy.GetComponent<RoamingEnemy>().KilledAction += RespawnEnemy;
    }
}
