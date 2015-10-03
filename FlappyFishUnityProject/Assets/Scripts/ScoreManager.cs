using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager _instance;

    private GameObject player;

    private float nextScoreX = 3.0f;
    private float scoreThreshold = 3.0f;
    private const float scoreThresholdClimber = 0.0f;

    private int score;
    public float Score
    {
        get
        {
            return score;
        }
    }
    private const float scoreIncrement = 1.0f;

    private int progress;
    public float Progress
    {
        get
        {
            return progress;
        }
    }

    private const float playersBaseSpeed = 1.8f;
    private float playersForwardSpeed;
    public float PlayersForwardSpeed
    {
        get
        {
            return playersForwardSpeed;
        }
    }
    private const float playerForwardAcceleration = 0.7f;


    private int scoreMultiplier = 1;
    public int ScoreMultiplier
    {
        get
        {
            return scoreMultiplier;
        }
    }

    private const int coinBoostToScoreMultiplier = 1;

    // UI elements
    private Text scoreText;
    private GameObject scoreTextObject;
    private GameObject retryTextObject;
    private GameObject highScoreTextObject;
	private GameObject comboScoreTextObject;
	private Text comboScoreText;


    private GameObject speedometerObject;
    private GameObject speedometerSet1Object;
    private GameObject speedometerSet2Object;
    private GameObject speedometerSet3Object;
    public Sprite EmptySpeedPip;
    public Sprite FilledSpeedPip;


    private bool scoreScreenAnimating;
    public bool ScoreScreenAnimating
    {
        get
        {
            return scoreScreenAnimating;
        }
    }



    public static ScoreManager instance
    {
        get
        {
            // if it hasn't been initialized, initialize
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ScoreManager>();
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        retryTextObject = GameObject.Find("RestartInstructions");
        highScoreTextObject = GameObject.Find("HighScoreText");
        scoreTextObject = GameObject.Find("ScoreText");
        scoreText = scoreTextObject.GetComponent<Text>();

		comboScoreTextObject = GameObject.Find("ComboText");
		comboScoreText = comboScoreTextObject.GetComponent<Text> ();

        speedometerObject = GameObject.Find("Speedometer");
        speedometerSet1Object = GameObject.Find("SpeedometerSet1");
        speedometerSet2Object = GameObject.Find("SpeedometerSet2");
        speedometerSet3Object = GameObject.Find("SpeedometerSet3");

        scoreTextObject.SetActive(false);
		comboScoreTextObject.SetActive (false);
        retryTextObject.SetActive(false);
        highScoreTextObject.SetActive(false);

        speedometerObject.SetActive(false);
        speedometerSet2Object.SetActive(false);
        speedometerSet3Object.SetActive(false);

        playersForwardSpeed = playersBaseSpeed;


    }

    
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void PlayerScored()
    {
        // SCORE GAINED! PROGRESS
        if(!scoreTextObject.activeSelf)
        {
            scoreTextObject.SetActive(true);
        }
        
        score += (Mathf.FloorToInt(scoreIncrement)*scoreMultiplier);
        progress += Mathf.FloorToInt(scoreIncrement);
        
        
        //scoreThreshold += scoreThresholdClimber; // this is set to 0 for now
        //nextScoreX += scoreThreshold;
        scoreText.text = score.ToString();
    }

    public void GameOverScoreSequence()
    {
        if (!scoreTextObject.activeSelf)
        {
            scoreTextObject.SetActive(true);
            // TODO: This means they probably didn't Jump once, show a Help Text, and hide the 0 score
        }
        scoreScreenAnimating = true;


        // finally, do a high score check
        highScoreCheck();

    }

    public void DoneAnimatingScoreSequence()
    {
        scoreScreenAnimating = false;
        retryTextObject.SetActive(true);
        highScoreTextObject.SetActive(true);
        GameSingleton.instance.AllowGameToRestart();
    }

    private void highScoreCheck()
    {
        if(score > PersistentObjectManager.control.HighScore3)
        {
            // new high score...three possible cases
            if( score > PersistentObjectManager.control.HighScore1)
            {
                // new high score 1
                PersistentObjectManager.control.HighScore3 = PersistentObjectManager.control.HighScore2;
                PersistentObjectManager.control.HighScore2 = PersistentObjectManager.control.HighScore1;
                PersistentObjectManager.control.HighScore1 = score;
            }
            else if ( score > PersistentObjectManager.control.HighScore2)
            {
                // new high score 2
                PersistentObjectManager.control.HighScore3 = PersistentObjectManager.control.HighScore2;
                PersistentObjectManager.control.HighScore2 = score;
            }
            else
            {
                // new high score 3
                PersistentObjectManager.control.HighScore3 = score;
            }
        }
        PersistentObjectManager.control.Save();
    }

    public void CoinPickedUp()
    {
		if (scoreMultiplier == 1) 
		{
			// first multiplier bonus! show UI
			comboScoreTextObject.SetActive(true);
            speedometerObject.SetActive(true);
		}
        scoreMultiplier += coinBoostToScoreMultiplier;
        comboScoreText.text = (scoreMultiplier).ToString() + "x";
        playersForwardSpeed += playerForwardAcceleration;
        addSpeedPip(scoreMultiplier-1);
    }

    public void CoinMissed()
    {
        if (scoreMultiplier > 1)
        {
            playersForwardSpeed -= playerForwardAcceleration;
            scoreMultiplier -= coinBoostToScoreMultiplier;
            comboScoreText.text = (scoreMultiplier).ToString() + "x";
            removeSpeedPip(scoreMultiplier);

            if( scoreMultiplier == 1)
            {
                comboScoreTextObject.SetActive(false);
                speedometerObject.SetActive(false);
            }


        }
    }


    private void addSpeedPip(int i)
    {
        if (i >= 1 && i <= 9)
        {
            GameObject pip;
            if( i <= 3 )
                pip = speedometerSet1Object.transform.FindChild("Pip" + i.ToString()).gameObject;
            else if( i <= 6 )
                pip = speedometerSet2Object.transform.FindChild("Pip" + i.ToString()).gameObject;
            else
                pip = speedometerSet3Object.transform.FindChild("Pip" + i.ToString()).gameObject;

            Image img = pip.GetComponent<Image>();
            img.sprite = FilledSpeedPip;

            // Get all components of type Image that are children of this GameObject.
            //Image[] images = pip.GetComponentsInChildren<Image>();
            //images[0].sprite = FilledSpeedPip;

            if( i == 4)
                speedometerSet2Object.SetActive(true);
            
            if( i == 7)
                speedometerSet3Object.SetActive(true);

        }


    }
    
    private void removeSpeedPip(int i)
    {
        if (i >= 1 && i <= 9)
        {
            GameObject pip;
            if( i <= 3 )
                pip = speedometerSet1Object.transform.FindChild("Pip" + i.ToString()).gameObject;
            else if( i <= 6 )
                pip = speedometerSet2Object.transform.FindChild("Pip" + i.ToString()).gameObject;
            else
                pip = speedometerSet3Object.transform.FindChild("Pip" + i.ToString()).gameObject;

            Image img = pip.GetComponent<Image>();
            img.sprite = EmptySpeedPip;

            if( i == 4)
                speedometerSet2Object.SetActive(false);
            
            if( i == 7)
                speedometerSet3Object.SetActive(false);
        }
    }
}
