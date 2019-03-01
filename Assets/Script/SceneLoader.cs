using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

  public void LoadNextScene()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    Debug.Log("lets go to " + currentSceneIndex);
    SceneManager.LoadScene(currentSceneIndex + 1);
  }

  public void LoadStartScene()
  {

    SceneManager.LoadScene(0);
    // FindObjectOfType<GameStatus>().ResetGame();
  }

  public void GameOver(){
    SceneManager.LoadScene("GameOver");
  }

  public void QuitGame()
  {
    Application.Quit();
  }


}
