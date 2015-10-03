using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboScoreScript : MonoBehaviour {
	
	private RectTransform rectTransform;
	private Text combotScoreText;

	// Use this for initialization
	void Start ()
	{
		combotScoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(ScoreManager.instance.ScoreScreenAnimating)
		{
			#region score animating region
			/*
			float newY = combotScoreText.transform.position.y + yTransformRate;
			combotScoreText.transform.position = new Vector3(combotScoreText.transform.position.x, newY, combotScoreText.transform.position.z);
			
			// now accelerate our transform rate
			yTransformRate += yAcceleration;
			
			
			// now do scaling adjustments on the score text
			combotScoreText.fontSize += Mathf.FloorToInt(scalingGrowth);
			scalingGrowth += scalingGrowthAcceleration;
			
			
			// are we done animating?
			if (combotScoreText.transform.position.y <= minYPosForText)
			{
				//print("STOP :" + scoreText.transform.position.y.ToString());
				ScoreManager.instance.DoneAnimatingScoreSequence();
			}
			*/
			#endregion
		}
	}
}
