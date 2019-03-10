using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
  [SerializeField] GameObject ammoPrefab;
  [SerializeField] int projectileSpeed;
  [SerializeField] float projectileRate = .2f;
  [SerializeField] float fireRate = 1.2f;
  [SerializeField] AudioClip shootSound;
  Coroutine firingCoroutine;
  bool isFiring;
  // Start is called before the first frame update
  void Start()
  {

  }

  public void Fire()
  {
    GameObject laser = Instantiate(ammoPrefab, transform.position, Quaternion.identity) as GameObject;
    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, .7f);
  }



  public float GetFireRate()
  {
    return fireRate;
  }
  // Update is called once per frame
  void Update()
  {

  }
}
