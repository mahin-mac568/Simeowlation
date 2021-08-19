using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : Enemy
{
  [SerializeField] private float leftBound;
  [SerializeField] private float rightBound;
  [SerializeField] private float runSpeed;

  private bool goingLeft = true;
  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
    if ((this.gameObject.name).Contains("Pig")){
      runSpeed = Random.Range(5f,20f);
    }
  }

  // Update is called once per frame
  private void Update()
  {
    Move();
  }

  private void Move()
  {
    if (goingLeft)
    {
      if (transform.position.x > leftBound)
      {
        if (transform.localScale.x != 1)
        {
          transform.localScale = new Vector3(1, 1);
        }
        rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
      }
      else
      {
        goingLeft = false;
      }
    }
    else if (!goingLeft)
    {
      if (transform.position.x < rightBound)
      {
        if (transform.localScale.x != -1)
        {
          transform.localScale = new Vector3(-1, 1);
        }
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
      }
      else
      {
        goingLeft = true;
      }
    }
  }
}