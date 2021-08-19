using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
  // public GameObject thePlatform;
  [SerializeField] public GameObject pooledObj;
  [SerializeField] private int pooledAmount;
  private List<GameObject> pooledObjs;



  // Start is called before the first frame update
  void Awake()
  {
    pooledObjs = new List<GameObject>();
    for (int i = 0; i < pooledAmount; i++)
    {
      GameObject obj = (GameObject)Instantiate(pooledObj);
      obj.SetActive(false);
      pooledObjs.Add(obj);
    }
  }

  // Update is called once per frame
  public GameObject GetPooledObj()
  {
    for (int i = 0; i < pooledObjs.Count; i++)
    {
      if (!pooledObjs[i].activeInHierarchy)
      {
        return pooledObjs[i];
      }
    }
    GameObject obj = (GameObject)Instantiate(pooledObj);
    obj.SetActive(false);
    pooledObjs.Add(obj);
    return obj;
  }
}

