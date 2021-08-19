using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
  // Constants 
  public float topBound;
  public float bottomBound;
  [SerializeField] private float flightHeight = 10f;
  private bool goingUp = true;


  // Components 
  private Animator a;
  private Rigidbody2D rb;

  private GameObject collDestructPoint;

  // protected AudioSource death;

  // Start is called before the first frame update
  void Start()
  {
    a = GetComponent<Animator>();
    a.SetBool("flying", true);

    rb = GetComponent<Rigidbody2D>();

    collDestructPoint = GameObject.Find("CollectibleDestructionPoint");
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.x < collDestructPoint.transform.position.x)
    {
      this.gameObject.SetActive(false);
    }

    Move();
  }

  private void Move()
  {
    if (goingUp)
    {
      if (transform.position.y < topBound)
      {
        rb.velocity = new Vector2(rb.velocity.x, flightHeight);
      }
      else
      {
        goingUp = false;
        a.SetBool("flying", false);
      }
    }
    else if (!goingUp)
    {
      if (transform.position.y > bottomBound)
      {
        rb.velocity = new Vector2(rb.velocity.x, -flightHeight);
      }
      else
      {
        goingUp = true;
        a.SetBool("flying", true);
      }
    }
  }

  public void Booped()
  {
    a.SetTrigger("deaded");
    rb.velocity = Vector2.zero;
    rb.bodyType = RigidbodyType2D.Kinematic;
    GetComponent<Collider2D>().enabled = false;
  }

  public void Death()
  {
    this.gameObject.SetActive(false);
  }
}
