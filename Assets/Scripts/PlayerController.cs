using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    Vector2 moveDirection;
    public float jumpSpeed;
    private float speed = 5;

	public bool hasPosition = false;
	public Vector2 startPosition;
	public Vector2 serverPosition;
	private float startTime;
	private float distance;

	public bool shouldInterpolate = true;
    public int playerID;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (shouldInterpolate && hasPosition) {
			Interpolate ();
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
