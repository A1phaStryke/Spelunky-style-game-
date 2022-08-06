using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPos : MonoBehaviour
{
    // variables for if the player should be moved into position or has moved
    public float waitTime;
    private bool playerMoved;

    // Update is called once per frame
    void Update()
    {
        // checks if the player has moved
        if(playerMoved == false){
            //checks if the player should move
            if(waitTime <= 0 && playerMoved == false){
                //moves player into position
                Vector2 playerPos = new Vector2(transform.position.x, transform.position.y + 2);
                transform.position = playerPos;
                playerMoved = true;
                return;
            } else {
                //lower the wait time with how long the game has been running
                waitTime -= Time.deltaTime;
            }
        }
    }
}
