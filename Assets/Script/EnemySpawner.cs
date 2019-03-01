﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] bool looping = false;
  [SerializeField] int totalEnemies = 0;
  int startingWave = 0;
  Level level;

  // Start is called before the first frame update
  private IEnumerator Start()
  {
    level = FindObjectOfType<Level>();
    for (int i = 0; i < waveConfigs.Count; i++)
    {
      totalEnemies = waveConfigs.Count * waveConfigs[i].GetNumberOfEnemies();
    }
    level.CountEnemiesLeft(totalEnemies);
    yield return new WaitForSeconds(1);
    StartCoroutine(SpawnAllWaves());
  }

  private IEnumerator SpawnAllWaves()
  {
    for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
    {
      var currentWave = waveConfigs[waveIndex];
      yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }
  }

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
  {
    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
    {
      var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
      newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
    }
  }
  // Update is called once per frame
  public int ReturnWaveNumber()
  {
    return waveConfigs.Count;
  }
  void Update()
  {
  }
}
