using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
  float randomNum;
  [SerializeField] GameObject bonusItem;
  [SerializeField] float dropChance = 10f;
  [SerializeField] float dropSpeed = 5f;

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

  // Update is called once per frame

}
