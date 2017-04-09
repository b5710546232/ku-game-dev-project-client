using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	float offset = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.GetComponent<UserController> () || col.gameObject.GetComponent<PlayerController> ()) {
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 10 );

			//col.gameObject.transform.position = new Vector3 (this.transform.position.x+offset, col.gameObject.transform.position.y,col.gameObject.transform.position.z );
		}
	}
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.GetComponent<UserController> () || col.gameObject.GetComponent<PlayerController> ()) {
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right *10 );
			//col.gameObject.transform.position = new Vector3 (this.transform.position.x+offset, col.gameObject.transform.position.y,col.gameObject.transform.position.z );
		}
	}
}
