using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotGenerator : MonoBehaviour
{
  [SerializeField] private ObjectPooler robotPool;
  private float robotGen;
  [SerializeField] private float upper;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  public void SpawnRobot(Vector3 startPosition)
  {
    robotGen = Random.Range(0f, 1f);

    if (robotGen > .5f)
    {
      GameObject robot = robotPool.GetPooledObj();
      robot.tag = "Enemy";
      robot.transform.position = startPosition;
      robot.SetActive(true);
      robot.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
      robot.GetComponent<Collider2D>().enabled = true;

      Robot r = robot.GetComponent<Robot>();
      r.bottomBound = startPosition.y;
      r.topBound = startPosition.y + upper;
    }
  }
}
