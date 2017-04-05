using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public int id = -1;

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	void OnTriggerEnter2D(Collider2D col) {
//		Debug.Log (string.Format ("Client#{0}, BulletOf#{1}", DGTRemote.Instance.gameManager.owner_id, id));
		if (id == GameManager.GetInstance ().owner_id) {
			Debug.Log ("Hit yourself do nothing");
		} else {
			Debug.Log (string.Format ("Bullet of {0} hit someone", id));
		}
	}
}
