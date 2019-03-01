using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  [SerializeField] float MovementSpeed = 10f;
  [SerializeField] float padding = 1f;
  [SerializeField] GameObject laserPrefab;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] float projectileRate = 0.1f;
  [SerializeField] int health = 500;
  [SerializeField] float delayInSeconds = 2f;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip hitSound;
  Weapon1 weapon;
  float xMin;
  float xMax;
  float yMin;
  float yMax;
  Coroutine firingCoroutine;
  // Start is called before the first frame update
  void Start()
  {
    weapon = GetComponent<Weapon1>() as Weapon1;
    SetUpMoveBoundary();
  }


  // Update is called once per frame
  void Update()
  {
    move();
    fire();
  }

  private void fire()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      weapon.Fire();
      //stantitate a laser prefab at the players transform.position, and do not rotate
      // firingCoroutine = StartCoroutine(FireContinuously());
    }
    if (Input.GetButtonUp("Fire1"))
    {
      weapon.StopFire();
    }
  }


  private void SetUpMoveBoundary()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
  }

  private void move()
  {
    float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
    float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;
    var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
    transform.position = new Vector2(newXPos, newYPos);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

    if (!damageDealer) { return; }
    health -= damageDealer.GetDamage();
    FindObjectOfType<GameStatus>().PlayerHealthChange(-damageDealer.GetDamage());
    AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, .4f);

    if (health <= 0)
    {
      GameOver();
      AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, .7f);
    }
  }

  public int GetHealth()
  {
    return health;
  }

  void GameOver()
  {
    Debug.Log("game over");
    StartCoroutine(WaitAndLoad());
  }

  IEnumerator WaitAndLoad()
  {
    Debug.Log("hello");
    yield return new WaitForSeconds(2);
    Destroy(gameObject);

    Debug.Log("whats up");
    SceneManager.LoadScene("GameOver");
  }
}

