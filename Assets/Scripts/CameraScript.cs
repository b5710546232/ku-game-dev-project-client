using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private GameObject[] players;
	private GameObject player;

	// Use this for initialization
	void Start () {
		FindPlayer ();
	
	}
	void FindPlayer(){

		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject playerInstance in players){
			if (playerInstance.GetComponent<UserController>()) {
				this.player = playerInstance;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (player) {
			Vector3 pos = new Vector3 (player.transform.position.x , player.transform.position.y ,-10f);
			this.transform.position = pos;


		} else {
			FindPlayer ();
		}
	}
}
