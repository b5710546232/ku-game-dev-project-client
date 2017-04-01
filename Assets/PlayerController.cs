using System.Collections;
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
		ShootTarget ();
    }

	void ShootTarget(){
		if (Input.GetMouseButtonDown (0)) {
			Vector2 target = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
			Vector2 curPos = new Vector2 (transform.position.x, transform.position.y);
			Vector2 dirention = target - curPos;
			dirention.Normalize ();
			Quaternion bulletRotation = Quaternion.Euler (0, 0, Mathf.Atan2 (dirention.y, dirention.x) * Mathf.Rad2Deg + 180);
			GameObject shootingBullet = (GameObject)Instantiate (bullet, curPos, bulletRotation);
			shootingBullet.GetComponent<Rigidbody2D> ().velocity = dirention * bulletSpeed;
			
		}
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
	
}
