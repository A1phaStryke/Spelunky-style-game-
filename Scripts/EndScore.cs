using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScore : MonoBehaviour
{
    //Variables for the score
    private ExitDoor Door;
    public TextMeshProUGUI scoreText;

    //variables for the transition
    public float transitionTime;
    public GameObject transition;

    //variables for making sure the game has ended
    private Score score;

    // Update is called once per frame
    void Update()
    {
        //displays the final score
        scoreText.text = "Score:" + ExitDoor.gameScore.ToString();

        //plays the transition
        if(Score.gameTime <= 0){
            StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(false));
        }
    }

}
