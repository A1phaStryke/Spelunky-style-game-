using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //variables for the transition
    public GameObject transition;
    public float waitTime;
    private bool transitioned = false;
    public float transitionTime;
    public float transitionedTime;
    public GameObject blackOutSquare;
    
    //variables for checking if the play or restart button has been clicked
    private bool PlayClicked = false;
    private bool RestartClicked = false;
    


    void Update(){
        //checks if the play button has been clicked and the transition has occured and plays the transition into the next scene
        if(PlayClicked == true && transitioned == true){
            //sets the transition time to 1 to allow the second transition to play
            transitionedTime = 1;
            transitionTime = 1;
            //checks if the second transition has been played
            if(waitTime <= 0 && transitioned == true){
                //loads the next scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            } else {
                //lower the wait time with how long the game has been running
                waitTime -= Time.deltaTime;
            }
        }

        //checks if the play button has been clicked and the transition has occured and plays the transition into the next scene
        if(RestartClicked == true && transitioned == true){
            //sets the transition time to 1 to allow the second transition to play
            transitionedTime = 1;
            transitionTime = 1;
            //checks if the second transition has been played
            if(waitTime <= 0 && transitioned == true){
                //loads the menu scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
            } else {
                //lower the wait time with how long the game has been running
                waitTime -= Time.deltaTime;
            }
        }

        if(transitionTime <= 0){
            //start transition
            StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(false));
            //check if transition has been finished
            if(transitionedTime <= 0){
                //disable the fade
                blackOutSquare.SetActive(false);
            } else {
                //lower the transitioned time with how long the game has been running
                transitionedTime -= Time.deltaTime;
            }
        } else {
                //lower the transition time with how long the game has been running
                transitionTime -= Time.deltaTime;
        }
    }

    public void PlayGame (){
        //start transiton and the timer for loading the next scene
        StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(true));
        transitioned = true;
        PlayClicked = true;
        //enable the fade
        blackOutSquare.SetActive(true);
    }

    public void QuitGame(){
        //quit the game
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void RestartGame(){
        //start transiton and the timer for loading the first scene
        RestartClicked = true;
        transitioned = true;
        StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(true));
        //enable the fade
        blackOutSquare.SetActive(true);
    }

}
