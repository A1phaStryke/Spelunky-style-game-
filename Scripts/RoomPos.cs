using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPos : MonoBehaviour {

    //variable for waiting to spawn the rest of the rooms
    public float waitTime;

    //variables for checking if there is a room in that position
    public LayerMask whatIsRoom;
    bool hasRoom;

    //variable for the closed room
    public GameObject closedRoom;

	void Update () {

        //sets the collider so that it can check if there is a room in that position
        Collider2D room = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        //checks if there is a room in that position
        if (room != null) {
            hasRoom = true;
        }

        //checks if rooms should be spawned
        if (waitTime <= 0)
        {
            //checks if rooms should be spawned in that position
            if (hasRoom == false) {
                //spawns rooms
                Instantiate(closedRoom, transform.position, Quaternion.identity);
            }
        }
        else {
            //lower the wait time with how long the game has been running
            waitTime -= Time.deltaTime;
        }
	}
}
