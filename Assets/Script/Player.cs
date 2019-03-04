using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  [SerializeField] float MovementSpeed = 10f;
  [SerializeField] float padding = 1f;

  [SerializeField] int health = 500;
  [SerializeField] float delayInSeconds = 2f;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip hitSound;
  [SerializeField] AudioClip getCoin;

  Weapon weapon;
  Weapon1 weapon1;
  GameStatus gameStatus;
  float xMin;
  float xMax;
  float yMin;
  float yMax;
  Coroutine firingCoroutine;
  [SerializeField] string currentWeapon = "Basic";
  // Start is called before the first frame update
  void Awake()
  {
    SetUpSingleTon();
  }

  private void SetUpSingleTon()
  {
    if (FindObjectsOfType(GetType()).Length > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }


  void Start()
  {
    weapon = GetComponent<Weapon>() as Weapon;
    weapon1 = GetComponent<Weapon1>() as Weapon1;
    gameStatus = FindObjectOfType<GameStatus>();
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
      if (currentWeapon == "Basic")
      {
        weapon.Fire();
      }
      if (currentWeapon == "SpreadShot")
      {
        weapon1.Fire();
      }
      //stantitate a laser prefab at the players transform.position, and do not rotate
      // firingCoroutine = StartCoroutine(FireContinuously());
    }

    if (Input.GetButtonUp("Fire1"))
    {
      if (currentWeapon == "Basic")
      {
        weapon.StopFire();
      }
      if (currentWeapon == "SpreadShot")
      {
        weapon1.StopFire();
      }
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
    if (other.gameObject.tag == "Coin")
    {
      print("touching coin");
      AudioSource.PlayClipAtPoint(getCoin, Camera.main.transform.position, .7f);
      gameStatus.PlayerGetCoin(10);
      Destroy(other.gameObject);
    }

    if (other.gameObject.tag == "EnemyFire")
    {
      DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

      if (!damageDealer) { return; }
      health -= damageDealer.GetDamage();
      gameStatus.PlayerHealthChange(-damageDealer.GetDamage());

      AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, .5f);
      Destroy(other.gameObject);
      if (health <= 0)
      {
        GameOver();
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, .6f);
      }
    }

    if (other.gameObject.tag == "SpreadShot")
    {
      weapon.StopFire();
      currentWeapon = "SpreadShot";
      Destroy(other.gameObject);
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

