using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{

    // Use this for initialization
    Vector2 moveDirection;
    public float jumpSpeed;
    private float speed = 5;

	public float position_x;
	public float position_y;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
		UpdatePosition();
		
    }

	void UpdatePosition(){
		position_x = gameObject.transform.position.x;
		position_y = gameObject.transform.position.y;
        if (Input.GetAxis("Horizontal") != 0.00f||Input.GetAxis("Vertical") !=0.00f)
		DGTRemote.GetInstance ().RequestMovementPlayer(position_x,position_y);
	}
    void Controller()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        PlayerFlip();
        Move(moveDirection);
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
}
