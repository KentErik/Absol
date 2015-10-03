using UnityEngine;
using System.Collections;

public class CoinPassDetector : MonoBehaviour {

    private bool areWeDying;
    public GameObject coinMissedSoundPrefab = null;


	// Use this for initialization
	void Start () {
        areWeDying = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (areWeDying)
        {
            //Color c = this.transform.parent.gameObject.GetComponent<SpriteRenderer>().material.color;
            Color c = this.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
            this.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = new Color(
                c.r,
                c.b, 
                c.g, 
                c.a - 0.2f);

            if(c.a <= 0.0f)
            {
                areWeDying = false;
                Destroy(this.transform.parent.gameObject);
            }
        }
	}


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //print("coin hit");
            //GameSingleton.instance.PlayerIsDead();
            
            //GameObject.Instantiate(coinParticleSystemPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            //GameObject.Instantiate(coinSoundPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            GameObject.Instantiate(coinMissedSoundPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            //GameSingleton.instance.AddAccelerationStreak();
            
            //ScoreManager.instance.CoinPickedUp();
            ScoreManager.instance.CoinMissed();
            //print("coin passed!");

            //GameSingleton.instance.ClearAccelerationStreaks();
            GameSingleton.instance.RemoveAccelerationStreak();


            //Destroy(this.gameObject);

            areWeDying = true;
            //StartCoroutine(FadeOut());

        }
    }

}
