using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this allows assets to be created when right clicking inside unity
[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{
  [SerializeField] GameObject enemyPrefab;
  [SerializeField] GameObject pathPrefab;
  [SerializeField] float timeBetweenSpawn = 0.5f;
  [SerializeField] float spawnRandom = 0.3f;
  [SerializeField] int numberOfEnemies = 5;
  [SerializeField] float moveSpeed = 2f; 

  public GameObject GetEnemyPrefab()
  {
    return enemyPrefab;
  }
  public List<Transform> GetWaypoints()
  {
    var waveWayPoints = new List<Transform>();
    foreach (Transform child in pathPrefab.transform)
    {
      waveWayPoints.Add(child);
    }

    return waveWayPoints;
  }
  public float GetTimeBetweenSpawn()
  {
    return timeBetweenSpawn;
  }
  public float GetSpawnRandom()
  {
    return spawnRandom;
  }
  public int GetNumberOfEnemies()
  {
    return numberOfEnemies;
  }
  public float GetMoveSpeed()
  {
    return moveSpeed;
  }


}
