using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
  [SerializeField] GameObject bar;
  [SerializeField] string target;
  float health;
  GameStatus gameStatus;
  // Start is called before the first frame update
  void Start()
  {
    gameStatus = FindObjectOfType<GameStatus>();

  }

  // Update is called once per frame
  void Update()
  {
    if (target == "Player")
    {
      health = gameStatus.GetPlayerHealth() / 1000;
    }
    else
    {
      health = gameStatus.GetOppPlayerHealth() / 1000;
    }
    bar.transform.localScale = new Vector3(health * 0.84f, 1f);

  }
}
