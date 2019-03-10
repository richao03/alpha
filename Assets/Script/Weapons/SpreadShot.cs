using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpreadShot : MonoBehaviour
{
  [SerializeField] GameObject ammoPrefab;
  [SerializeField] int projectileSpeed;
  [SerializeField] float fireRate = .2f;
  [SerializeField] AudioClip shootSound;
  Coroutine firingCoroutine;
  float lastShot = 0.0f;  // Start is called before the first frame update
  bool isFiring;
  void Start()
  {

  }

  public void Fire()
  {

    CreateBullet(-1f);
    CreateBullet(1f);

    // GameObject laser = Instantiate(ammoPrefab, transform.position, Quaternion.identity) as GameObject;
    // laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, .7f);

  }



  public float GetFireRate()
  {
    return fireRate;
  }

  private void CreateBullet(float angleOffset = 0f)
  {

    GameObject bullet = Instantiate<GameObject>(ammoPrefab, transform.position, Quaternion.Euler(Vector3.forward * -angleOffset));
    bullet.transform.position = transform.position;

    // Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(angleOffset, projectileSpeed);

    // rigidbody.AddForce(Quaternion.AngleAxis(angleOffset, Vector3.forward) * transform.right * 100.0f);
  }
  // Update is called once per frame
  void Update()
  {

  }
}
