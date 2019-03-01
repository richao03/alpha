using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
  // Start is called before the first frame update

  [SerializeField] int enemiesLeft;
  //cached refernce
  SceneLoader sceneLoader;
  private void Start()
  {
    sceneLoader = FindObjectOfType<SceneLoader>();
  }
  public void CountEnemiesLeft(int num)
  {
    enemiesLeft = num;
  }

  public void EnemyDestroyed()
  {
    enemiesLeft--;
    if (enemiesLeft <= 0)
    {
      StartCoroutine(EndLevel());
    }
  }

  private IEnumerator EndLevel()
  {
    yield return new WaitForSeconds(2);
    sceneLoader.LoadNextScene();
  }
}
