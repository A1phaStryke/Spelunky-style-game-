using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

	private LevelGeneration templates;

	void Start(){

		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelGeneration>();
		templates.roomsList.Add(this.gameObject);
	}
}