using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class PlayerController : MonoBehaviour
{
  // State 
  private enum State { idle, running, jumping, falling, justWon, justLost, hurt };
  private State st = State.running;

  // Constants 
  [SerializeField] private float mvSpeed;
  [SerializeField] private float jpForce;
  [SerializeField] private float jpPeak = 0.1f;

  private bool isGrounded;
  [SerializeField] private LayerMask ground;
  [SerializeField] private LayerMask enemy;

  [SerializeField] private Transform groundCheck;
  [SerializeField] private float checkRad;
  [SerializeField] private int lives;
  [SerializeField] private TextMeshProUGUI livesText;

  private float knockBack = 5f;


  // Components 
  private Rigidbody2D rb;
  private Animator anim;
  [SerializeField] private CircleCollider2D cc;
  private SpriteRenderer sr;

  // Jumping 
  [SerializeField] private float jpTime;
  private float jpTimeCount;

  // Collectibles 
  private int junkCollected = 0;
  [SerializeField] private TextMeshProUGUI junkText;
  [SerializeField] private int goalAmount = 1000;

  public int junkMissed = 0;
  public TextMeshProUGUI missedText;
  [SerializeField] private int missAmount = 1000;
  private bool slowDown = false;

  // Scenes 
  [SerializeField] private string winScene;
  [SerializeField] private string loseScene;
  [SerializeField] private string chadScene;


  // Audio 
  [SerializeField] private AudioSource ac;
  [SerializeField] private AudioSource genSound;
  [SerializeField] private AudioSource goalSound;
  [SerializeField] private AudioSource goalMusic;

  [SerializeField] private AudioSource catDeath;
  [SerializeField] private AudioSource robotDeath;



  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
    jpTimeCount = jpTime;

    livesText.text = lives.ToString();
  }

  // Update is called once per frame
  private void Update()
  {
    EndingCheck();

    isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRad, ground);

    if (st != State.hurt)
    {
      Movement();
      Jump();
    }

    ActionTransition();
    anim.SetInteger("state", (int)st);
  }

  private void Movement()
  {
    if (slowDown)
    {
      rb.velocity *= 0.95f;
      ac.volume *= 0.825f;
    }
    else
    {
      rb.velocity = new Vector2(mvSpeed, rb.velocity.y);
    }
  }

  private void Jump()
  {
    if (slowDown)
    {
      return;
    }
    if (!isGrounded && st == State.falling)
    {
      jpTimeCount = 0;
    }
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      rb.velocity = new Vector2(rb.velocity.x, jpForce);
      st = State.jumping;
    }
    if (Input.GetKey(KeyCode.Space) && jpTimeCount > 0)
    {
      rb.velocity = new Vector2(rb.velocity.x, jpForce);
      jpTimeCount -= Time.deltaTime;
      st = State.jumping;
    }
    if (Input.GetKeyUp(KeyCode.Space))
    {
      jpTimeCount = 0;
    }
    if (isGrounded)
    {
      jpTimeCount = jpTime;
    }
  }

  private void EndingCheck()
  {
    if (lives <= 0 && isGrounded)
    {
      slowDown = true;
      st = State.justLost;
      cc.radius = 0.15f;
      StartCoroutine(WaitForLoseScene());
    }
    else if (junkCollected >= goalAmount || junkMissed >= missAmount)
    {
      slowDown = true;
      if (rb.velocity.x <= 0.1f)
      {
        rb.velocity = Vector2.zero;
        if (junkCollected >= goalAmount)
        {
          st = State.justWon;
          StartCoroutine(WaitForWinScene());
        }
        else if (junkMissed >= missAmount && isGrounded)
        {
          st = State.justLost;
          cc.radius = 0.15f;
          StartCoroutine(WaitForLoseScene());
        }
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Chad_Shades(Clone)")
    {
      goalSound.Play();
      collision.gameObject.SetActive(false);
      slowDown = true;
      st = State.justWon;
      StartCoroutine(WaitForChadScene());
    }
    else if (collision.tag == "Collectible")
    {
      genSound.Play();
      collision.gameObject.SetActive(false);
      junkCollected += 1;
      junkText.text = junkCollected.ToString();
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "NotEnemy")
    {
      if (st == State.justWon || st == State.justLost)
      {
        return;
      }
      if (st == State.falling && transform.position.y > other.gameObject.transform.position.y)
      {
        other.gameObject.GetComponent<Robot>().Booped();
        robotDeath.Play();
      }
      else
      {
        if (other.gameObject.tag == "Enemy")
        {
          st = State.hurt;
          lives--;
          livesText.color = Color.red;
          livesText.text = lives.ToString();
          rb.velocity = new Vector2(-knockBack, rb.velocity.y);
          other.gameObject.tag = "NotEnemy";
          StartCoroutine(WaitForEnemyTag(other.gameObject));
        }
      }
    }
  }

  private void ActionTransition()
  {
    if (st == State.jumping)
    {
      if (rb.velocity.y < jpPeak)
      {
        st = State.falling;
      }
    }
    else if (!isGrounded && st != State.jumping)
    {
      st = State.falling;
    }
    else if (st == State.falling)
    {
      if (isGrounded)
      {
        st = State.running;
      }
    }
    else if (st == State.hurt)
    {
      if (Mathf.Abs(rb.velocity.x) < .1f)
      // && isGrounded)
      {
        st = State.running;
      }
    }
    else if (st == State.justWon)
    {

    }
    else if (st == State.justLost)
    {

    }
    else
    {
      st = State.running;
    }
  }

  private void NoMoreLives()
  {
    sr.enabled = false;
  }

  private void PlayCatExplosion()
  {
    catDeath.Play();
  }

  private void PlayGoalMusic()
  {
    goalMusic.Play();
  }

  private IEnumerator WaitForWinScene()
  {
    yield return new WaitForSeconds(3f);
    SceneManager.LoadScene(winScene);
  }

  private IEnumerator WaitForLoseScene()
  {
    yield return new WaitForSeconds(3f);
    SceneManager.LoadScene(loseScene);
  }

  private IEnumerator WaitForChadScene()
  {
    yield return new WaitForSeconds(3f);
    SceneManager.LoadScene(chadScene);
  }

  private IEnumerator WaitForEnemyTag(GameObject robot)
  {
    yield return new WaitForSeconds(1f);
    robot.tag = "Enemy";
    livesText.color = Color.white;
  }

}

