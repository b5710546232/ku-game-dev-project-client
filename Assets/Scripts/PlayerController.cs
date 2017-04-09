using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    Vector2 moveDirection;

	public GameObject player;
    public float jumpSpeed;
    private float speed = 5;

//	public bool hasPosition = false;
	public Vector2 startPosition;
	public Vector2 serverPosition;
	private float startTime;
	private float distance;

	public bool shouldInterpolate = false;
	public int id = -1;
	Animator animator;

    void Start()
    {
		animator = gameObject.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
//		if (shouldInterpolate && hasPosition) {
//			Interpolate ();
//		}
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
	public void PlayAnimate(float posX, float posY){
		if (animator) {
			if (posX != 0 || posY != 0) {
				animator.SetTrigger ("isWalking");
			} else if (posX == 0 && posY == 0) {
				animator.SetTrigger ("isIdle");
			} else {
			}
		}
	}
}
