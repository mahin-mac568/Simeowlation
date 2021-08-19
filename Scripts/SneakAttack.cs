using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAttack : MonoBehaviour
{
    [SerializeField] Enemy Mush; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Mush.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
