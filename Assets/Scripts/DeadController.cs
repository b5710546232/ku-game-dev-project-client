using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void PlayerDeath(){
		this.gameObject.SetActive (true);		
	}
	void PlayerRespawn(){
		this.gameObject.SetActive (false);
	}

	public void RequestRespawn() {
		Debug.Log ("Requesting respawn...");
		DGTRemote.Instance.RequestRespawn ();
		PlayerRespawn ();
	}

}
