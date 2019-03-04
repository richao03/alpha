using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppPlayer : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] int health = 100;
  [SerializeField] float shotCounter;
  [SerializeField] float speed = 10f;

  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 2f;
  [SerializeField] GameObject projectile;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] float durationOfExplosion = 2f;

  DropItem dropItem;

  [SerializeField] GameObject explosionParticles;
  [SerializeField] GameObject shotsHitParticles;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip hitSound;
  [SerializeField] AudioClip shootSound;
  Level level;
  Vector3 randomMovement;
  Vector3 direction;
  private float timeLeft;
  [SerializeField] float accelerationTime;
  private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

  void Start()
  {
    shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    level = FindObjectOfType<Level>();
    rb2d = GetComponent<Rigidbody2D>();
    accelerationTime = Random.Range(.6f, 2f);
  }

  void Update()
  {
    countDownAndShoot();
  }

  void FixedUpdate()
  {
    randomMove();
  }

  public int GetOppHealth()
  {
    return health;
  }

  private void randomMove()
  {
    Debug.Log("started moving");
    timeLeft -= Time.deltaTime;
    if (timeLeft <= 0)
    {
      timeLeft += accelerationTime;
      randomMovement = Random.insideUnitCircle;

    }
    direction = new Vector3(randomMovement.x * speed * 1.5f, randomMovement.y * speed, 0.0f);
    rb2d.velocity = direction * speed * Time.deltaTime;
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
    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, .7f);
    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (!damageDealer) { return; }
    if (other.gameObject.tag == "PlayerFire")
    {
      AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, .8f);
      health -= damageDealer.GetDamage();
      FindObjectOfType<GameStatus>().OppPlayerHealthChange(-damageDealer.GetDamage());
      GameObject hitExplosion = Instantiate(shotsHitParticles, transform.position, Quaternion.identity) as GameObject;
      Destroy(other.gameObject);
      Destroy(hitExplosion, 0.5f);
      if (health <= 0)
      {
        Death();
      }
    }
  }

  private void Death()
  {
    level.EnemyDestroyed();
    dropItem.TriggerDrop();
    Destroy(gameObject);
    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity) as GameObject;
    Destroy(explosion, durationOfExplosion);

    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, .7f);
  }
}
