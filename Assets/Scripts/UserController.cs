﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{

    // Use this for initialization
    Vector2 moveDirection;
    public float jumpSpeed;
    
	public float speed = 3f;

	public float position_x;
	public float position_y;

	private const float FRAME_INTERVAL = 0.1f;
	public float currentInterval = 0f;

	public bool hasPosition = false;
	public Vector2 startPosition;
	public Vector2 serverPosition;
	private float startTime;
	private float distance;

	public bool shouldInterpolate = true;

	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
//        Controller();
//		UpdatePosition();
		Controller ();
		if(currentInterval >= FRAME_INTERVAL) {
			DGTRemote.Instance.RequestPlayersInfo ();
			currentInterval = 0f;
		}
//		currentInterval += ((int)Time.deltaTime * 1000) / 1000f;
		currentInterval += Time.deltaTime;
    }

	void UpdatePosition(){
		position_x = gameObject.transform.position.x;
		position_y = gameObject.transform.position.y;
        if (Input.GetAxis("Horizontal") != 0.00f||Input.GetAxis("Vertical") !=0.00f)
		DGTRemote.GetInstance ().RequestMovementPlayer(position_x,position_y);
	}

    void Controller()
    {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
//		Debug.Log (string.Format ("h:{0}, v:{1}", h, v));
		DGTRemote.Instance.RequestInputAxes (h, v);

		if (shouldInterpolate && hasPosition) {
			Interpolate ();
		}

//        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//        PlayerFlip();
//        Move(moveDirection);
        // if (Input.GetButton("Jump"))
    }

    public void Move(Vector2 direction)
    {
        gameObject.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void PlayerFlip()
    {
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
    }

	public void startInterpolate() {
		startTime = Time.time;
		startPosition = gameObject.transform.position;
		distance = Vector2.Distance (startPosition, serverPosition);
	}

	private void Interpolate() {
		if (distance > 0) {
			float covered = (Time.time - startTime) * speed;
			float ratio = covered / distance;
			gameObject.transform.position = Vector2.Lerp (startPosition, serverPosition, ratio);
		}
	}
}
