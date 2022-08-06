using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitDoor : MonoBehaviour
{
    //variable for the game score and making sure it stays between map generations
    [SerializeField] static public float gameScore = 0;

    //variables for transition
    public float waitTime;
    public GameObject transition;
    private bool transitioned;
    public bool InteractDoor;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //finds the transition game object
        transition = GameObject.Find("Transition");

        //checks if the player is colliding with the door
        if(collision.tag == "Player")
            {
                StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(true));
                InteractDoor = true;
                transitioned = true;

            }
    }

    void Start(){
        //sets interact door to false
        InteractDoor = false;
    }

    void Update(){
            //checks if its time to re generate map
            if(waitTime <= 0 && transitioned == true){
                gameScore ++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else {
                waitTime -= Time.deltaTime;
            }
    }
}
