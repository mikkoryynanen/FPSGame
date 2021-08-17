using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    [SerializeField] float spawnRate = 1f;
    [SerializeField] GameObject obj;
    Transform[] _spawnpoints;
    int _enemiesLeft = 0;
    List<int> _filledIndexes = new List<int>();


    private void OnEnable() 
    {
        EventManager.Subscribe<OnEnemyKilled>(EnemyKilled);
    }

    private void OnDisable() 
    {
        EventManager.Unsubscribe<OnEnemyKilled>();
    }

    IEnumerator Start()
    {
        _spawnpoints = GetComponentsInChildren<Transform>();        

        YieldInstruction yi = new WaitForSeconds(spawnRate);       

        while (true)
        {
            SpawnWave(2);
            while (_enemiesLeft > 0)
            {
                Debug.Log("Waiting for enemies to die off!");
                yield return null;
            }
            yield return null;
        }
    }

    private void EnemyKilled(OnEnemyKilled obj)
    {
        _enemiesLeft--;
        Debug.Log($"enemy killed. {_enemiesLeft} left");
    }

    void SpawnWave(int enemiesCount)
    {
        _enemiesLeft = enemiesCount;

        for (int i = 0; i < enemiesCount; i++)
        {
            Instantiate(obj, _spawnpoints[i].position, Quaternion.identity);
        }
    }
}