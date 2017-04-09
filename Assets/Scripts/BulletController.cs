using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public int id = -1;

	public GameObject owner;

	public float lifeTimeLimit = 1f;
	public float lifeTimeStart;


	// Use this for initialization
	void Start () {
		lifeTimeStart = Time.time;	

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lifeTimeStart > lifeTimeLimit){
			gameObject.SetActive(false);
		}	
	}
	public void resetTo(Vector3 pos){
		lifeTimeStart = Time.time;
		transform.position = pos;
		gameObject.SetActive(true);
	}



	public void resetTo(Vector3 pos,Quaternion qua){
		lifeTimeStart = Time.time;
		transform.position = pos;
		transform.rotation = qua;
		gameObject.SetActive(true);
	}


	void OnTriggerEnter2D(Collider2D col) {
		GameObject client = DGTRemote.Instance.gameManager.client;
		if (owner != null && client != null && owner != col.gameObject) {
//			if (owner != col.gameObject ) {
			Debug.Log (string.Format ("Bullet of {0} hit {1}", owner.name, col.gameObject.name));
			if (owner == client) {
				PlayerController hitPlayer = col.gameObject.GetComponent<PlayerController> ();
				if (hitPlayer != null) {
					int index = hitPlayer.id;
					if (index >= 0) {
						DGTRemote.Instance.RequestProjectileHit (index);
						gameObject.SetActive(false);
					}
				}
			}
//				PlayerController hitPlayer = col.gameObject.GetComponent<PlayerController> ();
//				if (hitPlayer != null) {
//					int index = hitPlayer.id;
//					if (index >= 0) {
//						DGTRemote.Instance.RequestProjectileHit (index);
//						gameObject.SetActive(false);
//					}
//				} else {
//					Debug.Log (string.Format ("Bullet of {0} hit {1}", owner.name, col.gameObject.name));
//					UserController hitClient = col.gameObject.GetComponent<UserController> ();
//					int index = hitClient.id;
//					if (index >= 0) {
//						DGTRemote.Instance.RequestProjectileHit (index);
//						gameObject.SetActive(false);
//					}
//				}
//			}
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
