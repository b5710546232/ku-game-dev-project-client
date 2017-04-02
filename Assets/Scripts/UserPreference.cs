using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserPreference : MonoBehaviour {
	
	public static string preferedGun;

	// Use this for initialization
	void Start () {
		Debug.Log ("Current Preference Gun : " + preferedGun);
	}
	
	// Update is called once per frame
	void Update () {
		handleClicking ();
	}
	void handleClicking(){
		if(Input.GetMouseButtonDown(0)){
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit) {
				if ( hit.collider.gameObject == GameObject.Find("Handgun")){
					Debug.Log ("Handgun Clicked");
					preferedGun = "Handgun";
					SceneManager.LoadScene (2);

				}  
				else if(hit.collider.gameObject == GameObject.Find("Shotgun")) {
					Debug.Log ("Shotgun Clicked");
					preferedGun = "Shotgun";
					SceneManager.LoadScene (2);
				}
				else if(hit.collider.gameObject == GameObject.Find("Rifle")) {
					Debug.Log ("Rifle Clicked");
					preferedGun = "Rifle";
					SceneManager.LoadScene (2);
				}
			}

		}
	}
}
