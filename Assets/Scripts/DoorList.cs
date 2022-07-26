using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorList : MonoBehaviour
{

	private SpawnPoint Doors;

	void Start(){

		Doors = GameObject.FindGameObjectWithTag("Door").GetComponent<SpawnPoint>();
		Doors.DoorList.Add(this.gameObject);
	}
}
