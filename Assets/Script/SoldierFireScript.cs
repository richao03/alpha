using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierFireScript : MonoBehaviour
{
  [SerializeField] public float firstShotDelay = 14f;
  [SerializeField] public float fireRate = 3f;
  GameObject[] soldiers;
  float startingTime = 4f;
  // Start is called before the first frame update
  void Start()
  {
    startingTime = Time.time + firstShotDelay;
  }

  private void Fire()
  {
    foreach (GameObject soldier in soldiers)
    {
      soldier.GetComponent<Soldiers>().Fire();
    }
  }
  // Update is called once per frame
  void Update()
  {
    soldiers = GameObject.FindGameObjectsWithTag("Soldier");
    if (Time.time >= startingTime)
    {
      startingTime = Time.time + fireRate;
      Fire();
    }
  }
}
