using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
  private GameObject platDestructPoint;
  private PlayerController pc;

  // Start is called before the first frame update
  void Start()
  {
    platDestructPoint = GameObject.Find("PlatformDestructionPoint");
    pc = FindObjectOfType<PlayerController>();
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.x < platDestructPoint.transform.position.x)
    {
      this.gameObject.SetActive(false);
    }
  }
}
