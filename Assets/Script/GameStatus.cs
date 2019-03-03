using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] int pointsPerEnemy = 17;
  [SerializeField] int currentScore = 0;
  [SerializeField] int currentHealth = 200;
  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] TextMeshProUGUI healthText;
  Player player;
  //   [SerializeField] bool isAutoPlayEnabled;

  private void Awake()
  {
    int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
    player = FindObjectOfType<Player>();

    if (gameStatusCount > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }

  }
  private void Start()
  {
    scoreText.text = currentScore.ToString();
    healthText.text = "Health: " + player.GetHealth().ToString();
  }
  // Update is called once per frame
  void Update()
  {
  }

  public void AddToScore()
  {
    currentScore = currentScore + pointsPerEnemy;
    scoreText.text = currentScore.ToString();

  }

  public void PlayerHealthChange(int hp)
  {
    currentHealth = currentHealth + hp;
    healthText.text = "Health: " + currentHealth.ToString();
  }

  public void ResetGame()
  {
    Destroy(gameObject);
  }

  //   public bool IsAutoPlayEnabled()
  //   {
  //     return isAutoPlayEnabled;
  //   }
}
