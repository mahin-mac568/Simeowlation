using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  protected Animator a;
  protected Rigidbody2D rb;
  //   protected AudioSource death;
  // Start is called before the first frame update
  protected virtual void Start()
  {
    a = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    // death = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  private void Update()
  {

  }

  public void Booped()
  {
    a.SetTrigger("booped");
    // death.Play();
    rb.velocity = Vector2.zero;
    rb.bodyType = RigidbodyType2D.Kinematic;
    GetComponent<Collider2D>().enabled = false;
  }

  private void Death()
  {
    Destroy(this.gameObject, 0.1f);
  }
}
