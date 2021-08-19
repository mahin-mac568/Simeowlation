using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalObject : MonoBehaviour
{
  private Animator a;
  // Start is called before the first frame update
  void Start()
  {
    a = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void GoalSecured()
  {
    a.SetTrigger("secured");
    GetComponent<Collider2D>().enabled = false;
  }
}
