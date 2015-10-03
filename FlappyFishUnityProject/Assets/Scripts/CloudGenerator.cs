using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour {

    public GameObject cloudPrefab = null;
    public GameObject CloudHolder = null;
    private GameObject player;

    private float cloudMinYPos = 2.0f;
    private float cloudMaxYPos = 4.0f;

    private float cloudCreationRangeTolerance = 1.0f;
    private float cloudCreationXOffset = 8.0f;
    private float cloudCreationRollFrequency = 15.0f;

    private float rollForNextCloudX = 1.0f;
    private float rollForNextCloudY = 0.0f;

    public Sprite[] cloudSprites;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        createCloudBasedOnX(Random.Range(-1.0f, 2.0f));
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameSingleton.instance.IsGameOver)
        {
            if (player.transform.position.x >= rollForNextCloudX)
            {
                createCloudBasedOnX(rollForNextCloudX + cloudCreationXOffset);
                rollForNextCloudX += cloudCreationRollFrequency;
            }
        }

    }

    private void createCloudBasedOnX (float xPos)
    {
        float x = Random.Range(xPos - cloudCreationRangeTolerance, xPos + cloudCreationRangeTolerance);
        float y = Random.Range(cloudMinYPos, cloudMaxYPos);
        int spritePicker = Random.Range(0, 3);
        //print(spritePicker);
        GameObject tile = GameObject.Instantiate(cloudPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        tile.transform.SetParent(CloudHolder.transform);
        Rigidbody2D rb2d = tile.GetComponent<Rigidbody2D>();
        rb2d.velocity = (new Vector2(1.5f, 0f));
        tile.GetComponent<SpriteRenderer>().sprite = cloudSprites[spritePicker];
    }

    public static void stopClouds()
    {
        GameObject[] clouds;
        Rigidbody2D rb2d;
        clouds = GameObject.FindGameObjectsWithTag("Cloud");
        foreach (GameObject cloud in clouds)
        {
            rb2d = cloud.GetComponent<Rigidbody2D>();
            rb2d.velocity = (new Vector2(0f, 0f));
        }
        
    }
}
