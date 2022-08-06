using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPos : MonoBehaviour
{
    //variables for making sure the door moves when it should and if the door has already moved
    public float waitTime;
    private bool doorMoved;

    // Update is called once per frame
    void Update()
    {
        //checks if the door has moved
        if(doorMoved == false){
            if(waitTime <= 0 && doorMoved == false){
                //moves the door into a new position
                Vector2 doorPos = new Vector2(transform.position.x + 3, transform.position.y - 2);
                transform.position = doorPos;
                doorMoved = true;
                return;
            } else {
                //lower the wait time with how long the game has been running
                waitTime -= Time.deltaTime;
            }
        }
    }
}
