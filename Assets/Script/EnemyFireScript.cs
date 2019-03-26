using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFireScript : MonoBehaviour
{
  [SerializeField] public float firstShotDelay = 14f;
  [SerializeField] public float fireRate = 3f;
  GameObject[] enemies;
  float startingTime = 4f;
  // Start is called before the first frame update
  void Start()
  {
    startingTime = Time.time + firstShotDelay;
  }

  private void Fire()
  {
    foreach (GameObject enemy in enemies)
    {
      enemy.GetComponent<Enemy>().Fire();
    }
  }
  // Update is called once per frame
  void Update()
  {
    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    if (Time.time >= startingTime)
    {
      startingTime = Time.time + fireRate;
      Fire();
    }
  }
}
