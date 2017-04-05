using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
		//Vector2 direction = mouseScreenPosition - (Vector2)transform.position.normalized;
		//transform.up = new Vector2(direction.x , direction.y);
		//transform.LookAt(mouseScreenPosition);

		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle,Vector3.forward);
		Flip ();


	}
	void Flip(){
		if (Input.GetAxis("Horizontal") < -0.1f)
		{
			this.GetComponent<SpriteRenderer> ().flipY = true;
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (Input.GetAxis("Horizontal") > 0.1f)
		{
			this.GetComponent<SpriteRenderer> ().flipY = false;
			transform.localScale = new Vector3(1, 1, 1);
		}

	}
}
