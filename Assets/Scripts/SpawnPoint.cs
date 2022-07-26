using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    private GameObject Door;

    public GameObject[] objectsToSpawn;

    public List<GameObject> DoorList;



    int DeleteDoors;

    private bool spawnedDoor;
    
    private void Start()
    {
        int rand = Random.Range(0, objectsToSpawn.Length);

        GameObject instance = Instantiate(objectsToSpawn[rand], transform.position, Quaternion.identity);
        
        instance.transform.parent = transform;
            
            Debug.Log("DoorList.Count =" + DoorList.Count);

        // if (DoorList.Count >= 1) {
        //     for (int i = 0; i < DoorList.Count -1; i++) {
        //         Destroy(DoorList[i]);
        //     }
        // }


    }
}
