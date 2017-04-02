using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    Vector2 moveDirection;
    public float jumpSpeed;
    public float speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }
    public void Move(Vector2 direction)
    {
        gameObject.transform.Translate(direction.normalized*speed  * Time.deltaTime);
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
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
	}
}
