using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {


    private bool inputReceived;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        inputReceived = false;
        foreach (Touch touch in Input.touches)
        {
            if( touch.phase == TouchPhase.Began )
            {
                inputReceived = true;
                break;
            }
        }
        
        if (inputReceived == false && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
        {
            inputReceived = true;
        }

        if (inputReceived == true)
        {
            // start the game
            Application.LoadLevel("mainScene");
        }

	}
}
