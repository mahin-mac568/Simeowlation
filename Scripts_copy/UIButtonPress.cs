using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIButtonPress : MonoBehaviour
{
  private Animator anim;
  [SerializeField] private string scene;

  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void PressPlayButton()
  {
    anim.SetTrigger("playPressed");
  }

  public void PressExitButton()
  {
    anim.SetTrigger("exitPressed");
  }

  public void PressCreditsButton()
  {
    anim.SetTrigger("creditsPressed");
  }

  public void NextScene()
  {
    SceneManager.LoadScene(scene);
  }

  public void ExitGame()
  {
    Application.Quit();
  }
}
