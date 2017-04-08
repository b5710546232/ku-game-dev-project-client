using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public int id = -1;

	public GameObject owner;

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}
	public void resetTo(Vector3 pos){
		transform.position = pos;
		gameObject.SetActive(true);
	}

	public void resetTo(Vector3 pos,Quaternion qua){
		transform.position = pos;
		transform.rotation = qua;
		gameObject.SetActive(true);
	}


	void OnTriggerEnter2D(Collider2D col) {
		if (owner != null) {
			if (owner != col.gameObject) {
				Debug.Log (string.Format ("Bullet of {0} hit {1}", owner.name, col.gameObject.name));
				PlayerController hitPlayer = col.gameObject.GetComponent<PlayerController> ();
				if (hitPlayer != null) {
					int index = hitPlayer.id;
					if (index >= 0) {
						DGTRemote.Instance.RequestProjectileHit (index);
					}
				} else {
					UserController hitClient = col.gameObject.GetComponent<UserController> ();
					int index = hitClient.id;
					if (index >= 0) {
						DGTRemote.Instance.RequestProjectileHit (index);
					}
				}
			}
		}
//		col.gameObject.GetComponent<
////		Debug.Log (string.Format ("Client#{0}, BulletOf#{1}", DGTRemote.Instance.gameManager.owner_id, id));
//		if (id == DGTRemote.Instance.gameManager.owner_id) {
//			Debug.Log ("You aren't going to hit yourself, are you?");
//		} else {
//			Debug.Log (string.Format ("Bullet of {0} hit someone", id));
//			col.gameObject.SetActive (false);
////			Destroy (col.gameObject);
//		}
	}
}
