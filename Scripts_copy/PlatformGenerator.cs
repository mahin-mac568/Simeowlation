using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
  // New Platforms 
  [SerializeField] private Transform genPoint;
  private int platSelector;

  // Distance 
  [SerializeField] private float distBetween;
  [SerializeField] private float distBetweenMin;
  [SerializeField] private float distBetweenMax;

  // Platform Widths 
  private float platWidth;
  private float[] platWidths;

  // Platform Heights 
  private float minHgt;
  [SerializeField] private Transform maxHgtPt;
  private float maxHgt;
  [SerializeField] private float maxHgtDiff;
  private float hgtDiff;

  // Object Pools
  [SerializeField] private ObjectPooler[] theObjPools;

  // Collectible 
  [SerializeField] private CollectibleGenerator cg;
  [SerializeField] private RobotGenerator rg;
  [SerializeField] private float collectibleTheshold;



  // Start is called before the first frame update
  private void Start()
  {
    platWidths = new float[theObjPools.Length];
    for (int i = 0; i < theObjPools.Length; i++)
    {
      platWidths[i] = theObjPools[i].GetPooledObj().GetComponent<BoxCollider2D>().size.x;
    }
    minHgt = transform.position.y;
    maxHgt = maxHgtPt.position.y;
  }

  // Update is called once per frame
  private void Update()
  {
    if (transform.position.x < genPoint.position.x)
    {
      distBetween = Random.Range(distBetweenMin, distBetweenMax);
      platSelector = Random.Range(0, theObjPools.Length);
      hgtDiff = Random.Range(-maxHgtDiff, maxHgtDiff);

      if (hgtDiff > maxHgt)
      {
        hgtDiff = maxHgt;
      }
      else if (hgtDiff < minHgt)
      {
        hgtDiff = minHgt;
      }

      transform.position = new Vector3(transform.position.x + (platWidths[platSelector] / 2) + distBetween, hgtDiff, transform.position.z);

      GameObject newplat = theObjPools[platSelector].GetPooledObj();
      newplat.transform.position = transform.position;
      newplat.transform.rotation = transform.rotation;
      newplat.SetActive(true);

      if (Random.Range(0f, 100f) < collectibleTheshold)
      {
        cg.SpawnCollectibles(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
      }
      rg.SpawnRobot(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));

      transform.position = new Vector3(transform.position.x + (platWidths[platSelector] / 2), transform.position.y, transform.position.z);

    }
  }
}
