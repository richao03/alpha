﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  [SerializeField] float MovementSpeed = 10f;
  [SerializeField] float padding = 1f;
  [SerializeField] GameObject deathAnimation;
  [SerializeField] int health = 500;
  [SerializeField] float delayInSeconds = 2f;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip hitSound;
  [SerializeField] AudioClip getCoin;
  [SerializeField] float fireRate = 0.2F;
  GameObject explosion;
  private float nextFire = 0.0F;

  Weapon weapon;
  SpreadShot spreadShot;
  SpreadShot1 spreadShot1;
  Impale impale;
  GameStatus gameStatus;
  float xMin;
  float xMax;
  float yMin;
  float yMax;
  Camera gameCamera;
  Coroutine firingCoroutine;
  private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
  bool levelComplete = false;
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
    spreadShot = GetComponent<SpreadShot>() as SpreadShot;
    spreadShot1 = GetComponent<SpreadShot1>() as SpreadShot1;
    impale = GetComponent<Impale>() as Impale;
    gameCamera = Camera.main;
    rb2d = GetComponent<Rigidbody2D>();

    gameStatus = FindObjectOfType<GameStatus>();
    SetUpMoveBoundary();
  }


  // Update is called once per frame
  void Update()
  {
    if (levelComplete)
    {
      CompleteLevel();
    }
    else
    {
      move();
      fire();
    }
  }

  private void fire()
  {
    if (Input.GetButton("Fire1"))
    {
      if (Time.time > nextFire)
      {
        nextFire = Time.time + fireRate;
        if (currentWeapon == "Basic")
        {
          weapon.Fire();
        }
        if (currentWeapon == "SpreadShot")
        {
          spreadShot.Fire();
        }
        if (currentWeapon == "SpreadShot1")
        {
          spreadShot1.Fire();
        }
        if (currentWeapon == "Impale")
        {
          impale.Fire();
        }
      }
    }
  }


  private void SetUpMoveBoundary()
  {
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

  public void LevelComplete()
  {
    levelComplete = true;
  }
  public void CompleteLevel()
  {
    if (transform.position.x != -.5f)
    {
      transform.position = Vector3.MoveTowards(transform.position, new Vector2(-.5f, transform.position.y), MovementSpeed * Time.deltaTime);
    }
    else
    {
      transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, 10), MovementSpeed * Time.deltaTime);

    }
  }

  public void ItemLevelUp(string item)
  {
    if (item == "SpreadShot")
    {
      if (currentWeapon == "SpreadShot")
      {
        currentWeapon = "SpreadShot1";
      }
      else
      {
        currentWeapon = "SpreadShot";
      }
    }
    if (item == "Impale")
    {
      if (currentWeapon == "Impale")
      {
        currentWeapon = "Impale1";
      }
      else
      {
        currentWeapon = "Impale";
      }
    }
    if (item == "Health1")
    {
      health = 1000;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Coin")
    {
      AudioSource.PlayClipAtPoint(getCoin, Camera.main.transform.position, .7f);
      gameStatus.PlayerGetCoin(10);
      Destroy(other.gameObject);
    }

    if (other.gameObject.tag == "EnemyFire")
    {
      DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
      gameCamera.GetComponent<ScreenShake>().TriggerShake(.1f);
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

    if (other.gameObject.tag == "SpreadShotItem")
    {
      // weapon.StopFire();
      if (currentWeapon != "SpreadShot1")
      {
        currentWeapon = "SpreadShot";
      }
      Destroy(other.gameObject);
    }
    if (other.gameObject.tag == "Impale")
    {
      // weapon.StopFire();
      if (currentWeapon != "Impale1")
      {
        currentWeapon = "Impale";
      }
      Destroy(other.gameObject);
    }
  }

  public float GetHealth()
  {
    return health;
  }

  void GameOver()
  {
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    gameObject.GetComponent<BoxCollider2D>().enabled = false;
    explosion = Instantiate(deathAnimation, transform.position, Quaternion.identity) as GameObject;
    Debug.Log("game over");
    StartCoroutine(WaitAndLoad());
  }

  IEnumerator WaitAndLoad()
  {
    yield return new WaitForSeconds(.5f);
    Destroy(explosion);
    yield return new WaitForSeconds(2);
    Destroy(gameObject);
    Debug.Log("whats up");
    SceneManager.LoadScene("GameOver");
  }
}

