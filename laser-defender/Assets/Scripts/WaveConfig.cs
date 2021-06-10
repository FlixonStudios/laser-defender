using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyObject()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        
        return waveWaypoints;
    }
    public float TimeBetweenSpawns
    {
        get
        {
            return timeBetweenSpawns;
        }
    }
    public float SpawnRandomFactor
    {
        get
        {
            return spawnRandomFactor;
        }
    }
    public int NumberOfEnemies
    {
        get
        {
            return numberOfEnemies;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
}
