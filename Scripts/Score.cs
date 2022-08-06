using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //Variables for the Score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float waitTime;
    
    //the length of time the game should last
    static public float gameTime = 100;

    //the scripts for Level Generation and the end Door
    private ExitDoor endOfRoom;
    private LevelGeneration levelGen;
    
    //the scripts for the transition
    public float transitionTime;
    public GameObject transition;


    // Start is called before the first frame update
    void Start()
    {
        // Find the Level generation script
        levelGen = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        //wait until the door has spawned and find its script
        if(waitTime <= 0){
            endOfRoom = GameObject.FindGameObjectWithTag("Door").GetComponent<ExitDoor>();
            //add current score to the score text
            scoreText.text = "Score:" + ExitDoor.gameScore.ToString();
                //check if the level has generated or the player isnt touching the door and start the timer
                if(levelGen.levelGenerated == true || endOfRoom.InteractDoor == false) {
                    //check if the game should still be running and if not start the transition to the end screen
                    if(gameTime <= 0){
                        // start the transitiom
                        StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(true));
                            if(transitionTime <=0){
                                //when transition has finished load the next scene
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                            } else {
                                //lower the transition time with how long the game has been running
                                transitionTime -= Time.deltaTime;
                            }
                    } else {
                        //lower the game time with how long the game has been running and display it
                        gameTime -= Time.deltaTime;
                        TimerDisplay(gameTime);
                    }
                }
        } else {
            //lower the wait time with how long the game has been running
            waitTime -= Time.deltaTime;
        }



    }

    void TimerDisplay(float displayGameTime){
        //convert the game time to minutes and seconds
        float minutes = Mathf.FloorToInt(displayGameTime / 60);
        float Seconds = Mathf.FloorToInt(displayGameTime % 60);
        //display the Time
        timerText.text = "Time Remaining:" + string.Format("{0:00} : {1:00}", minutes, Seconds);
    }
}
