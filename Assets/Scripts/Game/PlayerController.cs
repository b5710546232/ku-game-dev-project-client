﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    Vector2 moveDirection;
    public float jumpSpeed;
    public float speed;
	public GameObject bullet;
	public float bulletSpeed;

	public GameObject shooter;

	public float fireRate = 0.2f;

    void Start()
    {
		shooter = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
		if (Input.GetMouseButtonDown(0)) {
			InvokeRepeating ("ShootTarget",0.00001f, fireRate);
		}

		if (Input.GetMouseButtonUp (0)) {
			CancelInvoke ("ShootTarget");
		}
    }

	void ShootTarget(){
		Vector2 target = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
		Vector2 curPos = new Vector2 (transform.position.x, transform.position.y);
		Vector2 dirention = target - curPos;
		dirention.Normalize ();
		Quaternion bulletRotation = Quaternion.Euler (0, 0, Mathf.Atan2 (dirention.y, dirention.x) * Mathf.Rad2Deg + 180);
		GameObject shootingBullet = (GameObject)Instantiate (bullet, curPos, bulletRotation);
		shootingBullet.GetComponent<Rigidbody2D> ().velocity = dirention * bulletSpeed;
	}
    void Controller()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		PlayerFlip();
		Move(moveDirection);
        // if (Input.GetButton("Jump"))
    }
	void PlayerFlip(){
		 if (Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
		
	}
	public void Move(Vector2 direction)
	{
		gameObject.transform.Translate(direction*speed  * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col){
		GameObject hitObj = col.gameObject;

		if(hitObj.GetComponent<Bullet>()){
			Debug.Log("HIT");
		}
			
	}
}
