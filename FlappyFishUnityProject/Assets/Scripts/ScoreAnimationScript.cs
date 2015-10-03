using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreAnimationScript : MonoBehaviour {

    private RectTransform rectTransform;
    private Text scoreText;

    private float minYPosForText = 350.0f;
    private float yTransformRate = -3.0f;
    private float yAcceleration = -0.2f;
    private float scalingGrowth = 1;
    private float scalingGrowthAcceleration = 0.3f;

    // Use this for initialization
    void Start ()
    {
        scoreText = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(ScoreManager.instance.ScoreScreenAnimating)
        {
            float newY = scoreText.transform.position.y + yTransformRate;
            scoreText.transform.position = new Vector3(scoreText.transform.position.x, newY, scoreText.transform.position.z);

            // now accelerate our transform rate
            yTransformRate += yAcceleration;


            // now do scaling adjustments on the score text
            scoreText.fontSize += Mathf.FloorToInt(scalingGrowth);
            scalingGrowth += scalingGrowthAcceleration;


            // are we done animating?
            if (scoreText.transform.position.y <= minYPosForText)
            {
                //print("STOP :" + scoreText.transform.position.y.ToString());
                ScoreManager.instance.DoneAnimatingScoreSequence();
            }

        }
    }
}
