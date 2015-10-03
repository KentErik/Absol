using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreTextScript : MonoBehaviour
{

    private GameObject HighScoreTextObject;
    private Text highScoreText;

    private bool textIsInvis = true;



    // Use this for initialization
    void Start()
    {
        HighScoreTextObject = GameObject.Find("HighScoreText");
        highScoreText = HighScoreTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSingleton.instance.CanStartGameOver)
        {
            textIsInvis = false;

            // update text
            highScoreText.text = "Top Score\n\n" +
                PersistentObjectManager.control.HighScore1 + "\n\n" +
                PersistentObjectManager.control.HighScore2 + "\n\n" +
                PersistentObjectManager.control.HighScore3 + "\n\n";

            HighScoreTextObject.SetActive(true);
        }
    }
}
