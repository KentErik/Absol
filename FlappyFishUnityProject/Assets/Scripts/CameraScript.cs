using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private Transform player; // reference to the play'ers transform
	private float cameraXOffset = 2.0f;


	// Use this for initialization
	void Awake()
    {
        // setup the reference
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(player.transform.position.x + cameraXOffset, transform.position.y, transform.position.z);
	}
}
