using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour {

    //variables for the rooms and the positions they can spawn at when starting
    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT

    // variables for the direction the room should move, if the generation is finished, and if the map has moved more than one room down because there is a glitch that makes the game unplayable if this variable didn't exist
    private int direction;
    private bool stopGeneration;
    private int downCounter;

    //variables how far the rooms should move and timers so that the rooms dont all spawn at once
    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    //variable for the player being able to collide with the rooms
    public LayerMask whatIsRoom;
    
    // variables for the rooms that have been spawned
    [Header("Spawned Rooms")]
    [SerializeField] private List<GameObject> roomsList;
    [SerializeField] private List<GameObject> lastRooms = new List<GameObject>();
    private GameObject _currentRoom;

    // variables for spawning the door when the generation has finished
    [Header("Doors")]
    [SerializeField]public GameObject Door;
    [SerializeField]public float waitTime;
    [SerializeField]private bool spawnedDoor;

    // variables for spawning the player when the generation has finished
    [Header("Player spawn")]
    [SerializeField]public GameObject Player;
    [SerializeField]private bool spawnedPlayer;

    // variables for the transition hiding the level generation
    public float transitionTime;
    public GameObject transition;
    private bool transitioned = false;

    // variable for checking if the level has generated and if the spawnpoints should be destroyed
    public bool levelGenerated;
    private GameObject spawnPoint;

    private void Start()
    {  
        // pick a random starting position for the rooms to start spawning
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        
        // spawn the rooms from an index and which direction the level should generate in
        SpawnRoomFromIndex(1);
        direction = Random.Range(1, 6);

        // set level generated to false
        levelGenerated = false;
    }

    private void Update()
    {
        //check if a room should spawn and spawn it
        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else {
            timeBtwSpawn -= Time.deltaTime;
        }
        
        //run the methods for spawning the door and the player
        SpawnDoor();
        SpawnPlayer();

            //check if the transition should be activated
            if(transitioned == false){
                if(transitionTime <= 0 && transitioned == false){
                    StartCoroutine(transition.GetComponent<Transition>().FadeBlackOutSquare(false));
                    transitioned = true;
                } else {
                    transitionTime -= Time.deltaTime;
                }
            }

    }

    //move the level generator in a random direction and spawn a room
    private void Move()
    {
        if (direction == 1 || direction == 2)
        { // Move right
          
            if (transform.position.x < 25)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                SpawnRoomFromIndex(randRoom);
                
                // Makes sure the level generator doesn't move left
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else 
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left
            if (transform.position.x > 0)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                SpawnRoomFromIndex(randRoom);
                
                direction = Random.Range(3, 6);
            }
            else 
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        { // MoveDown
            downCounter++;
            if (transform.position.y > -25)
            {
                // replace the room before going down with a room that has a down opening, so type 3 or 5
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
                Debug.Log(previousRoom);
                if (previousRoom.GetComponent<Room>().roomType != 4 && previousRoom.GetComponent<Room>().roomType != 2)
                {
                    // My problem : if the level generation goes down twice in a row, there's a chance that the previous room is just 
                    // a LRB, meaning there's no top opening for the other room

                    if (downCounter >= 2)
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        SpawnRoomFromIndex(4);
                    }
                    else
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2;
                        }

                        SpawnRoomFromIndex(randRoomDownOpening);
                    }
                }
                
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Makes sure the room we drop into has a top opening
                int randRoom = Random.Range(3, 5);
                SpawnRoomFromIndex(randRoom);
                
                direction = Random.Range(1, 6);
                
            }
            else 
            {
                levelGenerated = true;
                stopGeneration = true;


            //used for debugging
                //Do a loop here to get your check
                // foreach (var room in roomsList)
                // {
                //     if (room.name != "Closed Rooms(Clone)")
                //     {
                //         lastRooms.Add(room);
                //     }
                // }
            }
        }
    }

    /// <summary>Pass a int which matches the room you want to spawn.</summary>
    void SpawnRoomFromIndex(int roomIndex)
    {
        _currentRoom = Instantiate(rooms[roomIndex], transform.position, Quaternion.identity);
        roomsList.Add(_currentRoom);
    }

    void SpawnDoor(){

        //check if a door should spawn
        if(spawnedDoor == false){
            if(waitTime <= 0 && spawnedDoor == false){
                //makes sure the door spawns at the last room
                for (int i = 0; i < roomsList.Count; i++) {
                    //spawns the door
                    if(i == roomsList.Count-1){
                        Instantiate(Door, roomsList[i].transform.position, Quaternion.identity);
                        spawnedDoor = true;
                    }
                }
            } else {
                //lowers wait time with the time the game has been running
                waitTime -= Time.deltaTime;
            }
        }
    }

    void SpawnPlayer(){

        //check if a player should spawn
        if(waitTime <= 0 && spawnedPlayer == false){
            //spawn player at the first room
            Instantiate(Player, roomsList[0].transform.position, Quaternion.identity);
			spawnedPlayer = true;
            return;
        }
    }

//Working on this (Not part of the final product)
    // void DeleteSpawnPoints(){
    //     if(waitTime <= 0){
    //         Destroy(GameObject.FindWithTag("Spawnpoint"));
    //     }
    // }
}