using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] bool looping = false;
  [SerializeField] int totalEnemies = 0;
  [SerializeField] float timeBetweenLoop = 5f;
  int startingWave = 0;
  GameObject[] enemies;

  // Start is called before the first frame update
  private IEnumerator Start()
  {
    yield return new WaitForSeconds(1);
    SpawnAllWaves();
    InvokeRepeating("SpawnAllWaves", 1.0f, timeBetweenLoop);
  }

  private void SpawnAllWaves()
  {
    for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
    {
      var currentWave = waveConfigs[waveIndex];
      StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }
  }

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
  {

    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
    {
      {
        var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
        yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
      }
    }
  }

  bool CheckIfUnitExist(WaveConfig waveConfig)
  {
    // foreach (GameObject enemy in enemies)
    // {
    //   if (enemy.transform.position.y == waveConfig.GetWaypoints()[0].transform.position.y)
    //   {
    //     return false;
    //   }
    // }
    return true;

  }
  // Update is called once per frame
  public int ReturnWaveNumber()
  {
    return waveConfigs.Count;
  }
  void Update()
  {
    enemies = GameObject.FindGameObjectsWithTag("Enemy");

  }
}
