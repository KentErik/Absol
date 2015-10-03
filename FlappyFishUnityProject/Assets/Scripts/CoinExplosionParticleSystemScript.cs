using UnityEngine;
using System.Collections;

public class CoinExplosionParticleSystemScript : MonoBehaviour {

    private AudioSource coinAudioSource;


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
