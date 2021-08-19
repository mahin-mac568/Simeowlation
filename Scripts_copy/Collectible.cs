using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
// private ScoreManager theScoreManager; 

{
  private PlayerController thePlayer;
  private GameObject collDestructPoint;

  // Start is called before the first frame update
  void Start()
  {
    thePlayer = FindObjectOfType<PlayerController>();
    collDestructPoint = GameObject.Find("CollectibleDestructionPoint");
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.x < collDestructPoint.transform.position.x)
    {
      this.gameObject.SetActive(false);
      thePlayer.junkMissed++;
      thePlayer.missedText.text = thePlayer.junkMissed.ToString();
    }
  }
}
