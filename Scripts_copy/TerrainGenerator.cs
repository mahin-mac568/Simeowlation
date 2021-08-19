using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
  [SerializeField] private GameObject theTerrain;
  private float terrainWidth;
  [SerializeField] private Transform genPoint;
  [SerializeField] private ObjectPooler theTerrainPool;



  // Start is called before the first frame update
  void Start()
  {
    terrainWidth = theTerrain.GetComponent<BoxCollider2D>().size.x;
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.x < genPoint.position.x)
    {
      transform.position = new Vector3(transform.position.x + (terrainWidth / 2), transform.position.y, transform.position.z);

      GameObject newTerrain = theTerrainPool.GetPooledObj();
      newTerrain.transform.position = transform.position;
      newTerrain.transform.rotation = transform.rotation;
      newTerrain.SetActive(true);

      transform.position = new Vector3(transform.position.x + (terrainWidth / 2), transform.position.y, transform.position.z);
    }
  }
}
