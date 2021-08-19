using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public PlayerController Player;
  private Vector3 lastPlayerPos;
  private float distToMove;

  // Start is called before the first frame update
  private void Start()
  {
    Player = FindObjectOfType<PlayerController>();
    lastPlayerPos = Player.transform.position;
  }

  // Update is called once per frame
  private void Update()
  {
    distToMove = Player.transform.position.x - lastPlayerPos.x;
    transform.position = new Vector3(transform.position.x + distToMove, transform.position.y, transform.position.z);
    lastPlayerPos = Player.transform.position;
  }
}
