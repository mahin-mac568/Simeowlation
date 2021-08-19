using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    private Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            a.SetTrigger("collapse");
        }
    }

    private void snap(){
        Destroy(this.gameObject);
    }
}
