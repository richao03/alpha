using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
  float randomNum;
  [SerializeField] GameObject bonusItem;
  [SerializeField] float dropChance = 10f;
  [SerializeField] float dropSpeed = 5f;
  [SerializeField] GameObject coinSprite;
  [SerializeField] AudioClip coinSound;
  Vector2 randomDirection;
  Rigidbody2D coinBody;
  
  // Start is called before the first frame update
  void Start()
  {

  }

  public void TriggerDrop()
  {
    randomNum = Random.Range(1, 100);
    Debug.Log(randomNum % dropChance);

    if (randomNum % dropChance == 0)
    {
      GameObject ItemDropped = Instantiate(bonusItem, transform.position, Quaternion.identity) as GameObject;
      ItemDropped.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -dropSpeed);
    }


  }

  public void ShootCoin()
  {
    randomNum = Random.Range(1, 100);

    if (randomNum % 2 == 0)
    {
      StartCoroutine("DropCoin");
    }
  }

  private IEnumerator DropCoin()
  {
    AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position, .7f);

    GameObject coinDropped = Instantiate(coinSprite, transform.position, Quaternion.identity) as GameObject;
    coinBody = coinDropped.GetComponent<Rigidbody2D>();
    randomDirection = Random.insideUnitCircle;
    coinBody.velocity = new Vector2(randomDirection.x * 5, randomDirection.y * 5);
    yield return new WaitForSeconds(2f);

    coinBody.velocity = Vector3.zero;
  }
  // Update is called once per frame

}
