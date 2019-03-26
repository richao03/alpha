using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] int pointsPerEnemy = 17;
  [SerializeField] int currentScore = 0;
  [SerializeField] float currentHealth = 200;
  [SerializeField] int currentOppHealth = 200;
  [SerializeField] int currentCoinCount = 200;
  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] TextMeshProUGUI healthText;
  [SerializeField] TextMeshProUGUI coinText;
  [SerializeField] TextMeshProUGUI oppPlayerHealthText;

  Player player;
  OppPlayer oppPlayer;
  //   [SerializeField] bool isAutoPlayEnabled;

  private void Awake()
  {
    int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
    player = FindObjectOfType<Player>();
    oppPlayer = FindObjectOfType<OppPlayer>();
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
    currentHealth = player.GetHealth();
    scoreText.text = currentScore.ToString();
    currentOppHealth = oppPlayer.GetOppHealth();
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
  }

  public void PlayerGetCoin(int coinCount)
  {
    currentCoinCount = currentCoinCount + coinCount;
    coinText.text = "Gild: " + currentCoinCount.ToString();

  }


  public void PlayerGiveCoin(int coinCount)
  {
    currentCoinCount = currentCoinCount - coinCount;
    coinText.text = "Gild: " + currentCoinCount.ToString();

  }
  public float GetPlayerHealth()
  {
    return currentHealth;
  }
  public float GetOppPlayerHealth()
  {
    return currentOppHealth;
  }
  public void OppPlayerHealthChange(int hp)
  {
    currentOppHealth = currentOppHealth + hp;
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
