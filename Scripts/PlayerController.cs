using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rb;
  private Animator a;
  private Collider2D col;
  [SerializeField] private LayerMask ground;

  private enum State { idle, running, jumping, falling, hurt };
  private State st = State.idle;


  [SerializeField] private float pSpeed = 7f;
  [SerializeField] private float jForce = 15f;
  [SerializeField] private float isRunning = 2f;
  [SerializeField] private float endOfJump = 0.1f;
  [SerializeField] private float enemyForce = 20f;
  [SerializeField] private int collectibles = 0;

  [SerializeField] private TextMeshProUGUI collectiblesText;
  [SerializeField] private int enemiesKilled = 0;
  [SerializeField] private TextMeshProUGUI boopsText;
  [SerializeField] private int health = 100;
  [SerializeField] private TextMeshProUGUI healthPoints;
  [SerializeField] private int goalCollectibles;
  [SerializeField] private int goalEnemies;


  [SerializeField] private string nextScene;
  [SerializeField] private string endScene1;
  [SerializeField] private string endScene2;
  [SerializeField] private string endScene3;


  [SerializeField] private AudioSource collSound;
  [SerializeField] private AudioSource hurtSound;
  [SerializeField] private AudioSource enemyDeathSound;
  [SerializeField] private AudioSource goalSound;
  [SerializeField] private AudioSource levelMusic;




  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    a = GetComponent<Animator>();
    col = GetComponent<Collider2D>();
  }

  // Update is called once per frame
  private void Update()
  {
    if (st != State.hurt)
    {
      Movement();
    }
    AnimationState();
    a.SetInteger("state", (int)st);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.tag == "Shortcut"){
      transform.position = new Vector3(113f,10.65f,0f);
    }
    else if (collision.tag == "Collectible")
    {
      collSound.Play();
      Destroy(collision.gameObject);
      collectibles += 1;
      collectiblesText.text = collectibles.ToString() + "/" + goalCollectibles.ToString();
      if (collectibles > goalCollectibles)
      {
        collectiblesText.color = Color.red;
      }
    }
    if (collision.tag == "Goal")
    {
      goalSound.Play();
      GoalObject goal = collision.gameObject.GetComponent<GoalObject>();
      goal.GoalSecured();
      if (collectibles < goalCollectibles || enemiesKilled < goalEnemies)
      {
        nextScene = endScene3;
      }
      else if (collectibles > goalCollectibles || enemiesKilled > goalEnemies)
      {
        nextScene = endScene2;
      }
      else if (nextScene == "na"){
        nextScene = endScene1;
      }

          StartCoroutine(WaitForSceneLoad());
          }
          }
      // if ((collectibles == goalCollectibles && enemiesKilled == goalCollectibles) && SceneManager.GetActiveScene().name == "Dark")
      // {
      //   nextScene = endScene1;
      // }
      // else if (collectibles > goalCollectibles || enemiesKilled > goalEnemies)
      // {
      //   nextScene = endScene2;
      // }
      // else if (collectibles < goalCollectibles || enemiesKilled < goalEnemies)
      // {
      //   nextScene = endScene3;
      // }
      // else
      // {
      //   nextScene = darkScene;
      // }


  

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      Enemy enemy = other.gameObject.GetComponent<Enemy>();
      if (st == State.falling && transform.position.y > other.gameObject.transform.position.y)
      {
        enemyDeathSound.Play();
        enemy.Booped();
        enemiesKilled += 1;
        boopsText.text = enemiesKilled.ToString() + "/" + goalCollectibles.ToString();
        if (enemiesKilled > goalEnemies)
        {
          boopsText.color = Color.red;
        }
        Jump();
      }
      else
      {
        // Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();
        // enemyRB.velocity = new Vector2(enemyRB.velocity.x, enemyRB.velocity.y);
        if (other.gameObject.transform.position.x > transform.position.x)
        {
          rb.velocity = new Vector2(-enemyForce, rb.velocity.y);
        }
        else
        {
          rb.velocity = new Vector2(enemyForce, rb.velocity.y);
        }
        st = State.hurt;
        hurtSound.Play();
        health -= 25;
        healthPoints.text = health.ToString() + "%";
        if (health < 50)
        {
          healthPoints.color = Color.red;
        }
        if (health <= 0)
        {
          StartCoroutine(PlayerDeath());
        }
      }
    }
  }

  private void Movement()
  {
    float hDirection = Input.GetAxis("Horizontal");

    if (hDirection < 0)
    {
      rb.velocity = new Vector2(-pSpeed, rb.velocity.y);
      transform.localScale = new Vector2(-1, transform.localScale.y);
    }
    else if (hDirection > 0)
    {
      rb.velocity = new Vector2(pSpeed, rb.velocity.y);
      transform.localScale = new Vector2(1, transform.localScale.y);
    }

    if (Input.GetButtonDown("Jump") && col.IsTouchingLayers(ground))
    {
      Jump();
    }
  }

  private void Jump()
  {
    rb.velocity = new Vector2(rb.velocity.x, jForce);
    st = State.jumping;
  }

  private void AnimationState()
  {
    if (st == State.jumping)
    {
      if (rb.velocity.y < endOfJump)
      {
        st = State.falling;
      }
    }
    else if (st == State.falling)
    {
      if (col.IsTouchingLayers(ground))
      {
        st = State.idle;
      }
    }
    else if (st == State.hurt)
    {
      if (Mathf.Abs(rb.velocity.x) < .1f)
      {
        st = State.idle;
      }
    }
    else if (Mathf.Abs(rb.velocity.x) > isRunning && col.IsTouchingLayers(ground))
    {
      st = State.running;
    }
    else
    {
      st = State.idle;
    }
  }

  private IEnumerator WaitForSceneLoad()
  {
    yield return new WaitForSeconds(3f);
    SceneManager.LoadScene(nextScene);
  }

  private IEnumerator PlayerDeath()
  {
    yield return new WaitForSeconds(.5f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
