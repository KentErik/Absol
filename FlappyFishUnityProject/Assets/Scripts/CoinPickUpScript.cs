using UnityEngine;
using System.Collections;

public class CoinPickUpScript : MonoBehaviour {

    // prefabs
    public GameObject coinParticleSystemPrefab = null;
	public GameObject coinSoundPrefab = null;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //print("coin hit");
            //GameSingleton.instance.PlayerIsDead();

            //print("Coin Pick Up Script run!");

            GameObject.Instantiate(coinParticleSystemPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
			GameObject.Instantiate(coinSoundPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
			                       
            GameSingleton.instance.AddAccelerationStreak();

            ScoreManager.instance.CoinPickedUp();
            //Destroy(this.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }



    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
