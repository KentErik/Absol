using UnityEngine;
using System.Collections;

public class ScorePickUpScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //print("Player scored!");
            ScoreManager.instance.PlayerScored();
        }

    }


}       