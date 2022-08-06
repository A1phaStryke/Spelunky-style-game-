using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    //variable for checking the room type
    public int roomType;

    //destroys the previous room when the level generation wants it too
    public void RoomDestruction() {
    
        Destroy(gameObject);
    }
}
