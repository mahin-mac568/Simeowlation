using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : Enemy
{
  [SerializeField] private float topBound;
  [SerializeField] private float bottomBound;
  [SerializeField] private float flightSpeed = 10f;

  private bool goingUp = true;
  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
    if ((this.gameObject.name).Contains("Skull")){
      flightSpeed = Random.Range(5f,20f);
    }
  }

  // Update is called once per frame
  private void Update()
  {
    Move();
  }

  private void Move()
  {
    if (goingUp)
    {
      if (transform.position.y < topBound)
      {
        rb.velocity = new Vector2(rb.velocity.x, flightSpeed);
      }
      else
      {
        goingUp = false;
      }
    }
    else if (!goingUp)
    {
      if (transform.position.y > bottomBound)
      {
        rb.velocity = new Vector2(rb.velocity.x, -flightSpeed);
      }
      else
      {
        goingUp = true;
      }
    }
  }
}