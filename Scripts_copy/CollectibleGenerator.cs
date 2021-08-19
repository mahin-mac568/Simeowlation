using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGenerator : MonoBehaviour
{
  [SerializeField] private ObjectPooler[] collectiblePools;
  private float itemGen;
  [SerializeField] private float distanceBetween = 2.5f;


  private string item;

  private int itemToUse;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SpawnCollectibles(Vector3 startPosition)
  {
    itemGen = Random.Range(0f, 1f);

    if (itemGen < 0.3f)
    {
      item = "Gear";
    }
    else if (itemGen < 0.5f)
    {
      item = "Screws";
    }
    else if (itemGen < 0.7f)
    {
      item = "Wheel";
    }
    else if (itemGen < 0.85f)
    {
      item = "Floppy";
    }
    else if (itemGen < 0.95f)
    {
      item = "Nokia";
    }
    else
    {
      item = "Sunglasses";
    }

    ObjectPooler cur = null;
    for (int i = 0; i < collectiblePools.Length; i++)
    {
      cur = collectiblePools[i];
      if (cur.gameObject.tag == item)
      {
        itemToUse = i;
        break;
      }
    }
    GameObject coll1 = collectiblePools[itemToUse].GetPooledObj();
    coll1.transform.position = startPosition;
    coll1.SetActive(true);

    if (cur.gameObject.tag != "Sunglasses")
    {
      GameObject coll2 = collectiblePools[itemToUse].GetPooledObj();
      coll2.transform.position = new Vector3(startPosition.x - distanceBetween, startPosition.y, startPosition.z);
      coll2.SetActive(true);

      GameObject coll3 = collectiblePools[itemToUse].GetPooledObj();
      coll3.transform.position = new Vector3(startPosition.x + distanceBetween, startPosition.y, startPosition.z);
      coll3.SetActive(true);
    }
  }
}
