using UnityEngine;
using System.Collections;

public class CoinPickedUpSoundScript : MonoBehaviour {

	private AudioSource coinAudioSource;
	
    // NOTE: This script is used by both the coin picked up and the coin missed sounds!
	
	// Use this for initialization
	void Start ()
	{
		
		coinAudioSource = GetComponent<AudioSource>();
		coinAudioSource.Play();
		Invoke("cleanMyselfUp", 2.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	
	
	void cleanMyselfUp()
	{
		Destroy(this.gameObject);
	}
}
