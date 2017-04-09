using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {


	Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {




	}

	public void ShootAnimation(){
		anim.SetTrigger ("isAttack");
		anim.SetTrigger ("isIdle");
	}
}
