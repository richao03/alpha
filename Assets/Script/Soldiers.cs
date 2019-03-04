using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] float health = 100;
  [SerializeField] float shotCounter;

  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 2f;
  [SerializeField] GameObject projectile;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] float durationOfExplosion = 2f;

  // // DropItem dropItem;

  [SerializeField] GameObject explosionParticles;
  [SerializeField] GameObject shotsHitParticles;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip hitSound;
  [SerializeField] AudioClip shootSound;
  Level level;

  void Start()
  {
    shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    level = FindObjectOfType<Level>();
    // // dropItem = GetComponent<DropItem>();

  }

  void Update()
  {
    countDownAndShoot();
  }

  private void countDownAndShoot()
  {
    shotCounter -= Time.deltaTime;
    if (shotCounter <= 0f)
    {
      Fire();
      shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
  }

  private void Fire()
  {
    GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, .2f);
    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (!damageDealer) { return; }
    if (other.gameObject.tag == "EnemyFire")
    {
      Debug.Log(other);
      Destroy(other.gameObject);
      health -= damageDealer.GetDamage();
      GameObject hitExplosion = Instantiate(shotsHitParticles, transform.position, Quaternion.identity) as GameObject;
      AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, .4f);
      Destroy(hitExplosion, 0.5f);
      if (health <= 0)
      {
        Death();
      }
    }
  }

  private void Death()
  {
    // dropItem.TriggerDrop();
    Destroy(gameObject);
    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity) as GameObject;
    Destroy(explosion, durationOfExplosion);

    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, .5f);
  }
}
