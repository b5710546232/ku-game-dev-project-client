using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public int numberPlayer;
	public GameObject player;
	public Dictionary<string, int> allPlayer;


	// Use this for initialization
	void Start () {
		//initialize dictionary
		allPlayer = new Dictionary<string,int> ();

		//added all player into dictionaary
		for(int i =0 ; i < numberPlayer; i++){
			allPlayer.Add ("ID Number "+i, i);
		}

		//create all player based on current dictionary
		for(int i =0 ; i < allPlayer.Count ; i++){
			Vector2 playerPos = new Vector2 (5+(i*2) , 8+(i*2)   );



			Instantiate (player,playerPos,Quaternion.identity);


		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
