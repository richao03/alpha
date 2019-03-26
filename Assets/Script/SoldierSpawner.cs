using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] bool looping = false;
  int startingWave = 0;
  [SerializeField] float timeBetweenLoop = 5f;
  GameObject[] soldiers;
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
      if (soldiers.Length < 12 && CheckIfUnitExist(waveConfig))
      {
        var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
        newEnemy.GetComponent<SoldierPath>().SetWaveConfig(waveConfig);
        yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
      }
    }
  }
  // Update is called once per frame
  bool CheckIfUnitExist(WaveConfig waveConfig)
  {
    // foreach (GameObject soldier in soldiers)
    // {
    //   print(soldier.transform.position.x);
    //   if (soldier.transform.position.x - waveConfig.GetWaypoints()[0].transform.position.x < 0.04f)
    //   {
    //     return false;
    //   }
    // }
    return true;

  }
  void Update()
  {
    soldiers = GameObject.FindGameObjectsWithTag("Soldier");

  }
}
