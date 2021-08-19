using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

  [SerializeField] private string gameScene;

  public void RestartGame()
  {
    SceneManager.LoadScene(gameScene);
  }

}
