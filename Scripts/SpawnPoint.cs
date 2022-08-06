using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    // checks which objects it should spawn
    public GameObject[] objectsToSpawn;
    
    private void Start()
    {

        // creates a random range of the objects in the array objectsToSpawn
        int rand = Random.Range(0, objectsToSpawn.Length);

        //spawns the objects
        GameObject instance = Instantiate(objectsToSpawn[rand], transform.position, Quaternion.identity);
        
        //spawns them at the same position as their parent object (The prefab these are all contained in)
        instance.transform.parent = transform;
    }
}

