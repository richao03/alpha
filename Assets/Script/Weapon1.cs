﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon1 : MonoBehaviour
{
  [SerializeField] GameObject ammoPrefab;
  [SerializeField] int projectileSpeed;
  [SerializeField] float projectileRate = .2f;
  [SerializeField] AudioClip shootSound;
  Coroutine firingCoroutine;

  // Start is called before the first frame update
  void Start()
  {

  }

  public void Fire()
  {

    firingCoroutine = StartCoroutine(FireContinuously());

  }

  public void StopFire()
  {
    StopCoroutine(firingCoroutine);
  }

  IEnumerator FireContinuously()
  {
    while (true)
    {
      // weapon.Fire;
      GameObject laser = Instantiate(ammoPrefab, transform.position, Quaternion.identity) as GameObject;
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, .4f);
      yield return new WaitForSeconds(projectileRate);
    }

  }
  // Update is called once per frame
  void Update()
  {

  }
}
