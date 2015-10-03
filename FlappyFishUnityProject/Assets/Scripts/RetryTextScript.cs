using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetryTextScript : MonoBehaviour
{
    private GameObject restartTextObject;
    private Text restartText;

    private bool textIsInvis = true;
    


    // Use this for initialization
    void Start()
    {
        restartTextObject = GameObject.Find("RestartInstructions");
        restartText = restartTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSingleton.instance.CanStartGameOver)
        {
            textIsInvis = false;
            restartTextObject.SetActive(true);
        }
    }

}
