using UnityEngine;
using System.Collections;

public class LeftSideConsumerScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //print("CONSUME " + collision.ToString());
        Destroy(collision.gameObject);
    }

}
