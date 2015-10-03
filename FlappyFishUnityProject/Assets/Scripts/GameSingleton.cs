using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameSingleton : MonoBehaviour {

    private static GameSingleton _instance;
    private GameObject player;

    public GameObject GameOverImagePrefab = null;

    private Stack<GameObject> accelerationStreakStack = null;
    public GameObject accelerationParticleSystemPrefab = null;

    private float startGameOverCooldown = 0.5f;
    private bool canStartGameOver;
    public bool CanStartGameOver
    {
        get
        {
            return canStartGameOver;
        }
    }

    private bool isGameOver;
    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }
    }


    public static GameSingleton instance
    {
        get
        {
            // if it hasn't been initialized, initialize
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameSingleton>();
            }
            return _instance;
        }
    }



	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isGameOver = false;
        accelerationStreakStack = new Stack<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    public void PlayerIsDead()
    {
        // set the variable
        isGameOver = true;

        // pause our player's motion
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        player.GetComponent<Rigidbody2D>().gravityScale = 0;

        // pause cloud movement
        CloudGenerator.stopClouds();


        /*
        This was the old method of doing the "you lost" graphic;

        // get the XYZ of the camera's look at point
        Vector3 camerasLookAtVec = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        camerasLookAtVec.z = 0;

        // make our temp 'retry' graphic at that point
        GameObject retryPrefab = Instantiate(GameOverImagePrefab, camerasLookAtVec, Quaternion.identity) as GameObject;
        */

        clearAccelerationStreaks();

        ScoreManager.instance.GameOverScoreSequence();

        //Invoke("AllowGameToRestart", startGameOverCooldown);

    }

    public void AllowGameToRestart()
    {
        canStartGameOver = true;

        // display the restart instructions???
    }

    public void RestartGame()
    {
        Application.LoadLevel("mainScene");
    }

    public void AddAccelerationStreak()
    {
        GameObject streak = GameObject.Instantiate(accelerationParticleSystemPrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
        streak.transform.SetParent(player.transform);
        Quaternion q = new Quaternion();
        q.SetLookRotation(Vector3.left, Vector3.up);
        streak.transform.rotation = q;
        accelerationStreakStack.Push(streak);
    }

    public void RemoveAccelerationStreak()
    {
        Destroy(accelerationStreakStack.Pop());
    }

    private void clearAccelerationStreaks()
    {
        while( accelerationStreakStack.Count > 0)
            Destroy(accelerationStreakStack.Pop());
    }

}
