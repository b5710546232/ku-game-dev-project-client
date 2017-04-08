using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject bulletHole;
	public GameObject bulletPrefab;

	public float bulletSpeed = 2f;
	public float shootInterval = 1f;
	public float currentInterval = 0f;

	private static Vector3 positiveScale = new Vector3 (1, 1, 1);
	private static Vector3 negativeScale = new Vector3 (-1, 1, 1);

	public bool isClient = false;

	// Use this for initialization
//	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (isClient) {
			TurnGun ();
			if (currentInterval >= shootInterval) {
				Shoot ();
			}
			currentInterval += Time.deltaTime;
		}
	}

	private void Shoot() {
		if (ControlFreak2.CF2Input.GetMouseButton (0)) {
			Vector2 target = Camera.main.ScreenToWorldPoint (new Vector2 (ControlFreak2.CF2Input.mousePosition.x, ControlFreak2.CF2Input.mousePosition.y));
			Vector2 direction = (target - (Vector2)bulletHole.transform.position);
			direction.Normalize ();
			Quaternion bulletRotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 180);
			SendBulletInfo (direction, bulletRotation);
//			GameObject bullet = Instantiate (bulletPrefab, bulletHole.transform.position, bulletRotation);
//			bullet.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
			currentInterval = 0f;
		}
	}

	private void TurnGun() {
//		Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
		Vector3 dir = ControlFreak2.CF2Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		Flip ();

	}

	private void Flip() {
		if (ControlFreak2.CF2Input.GetAxis("Horizontal") < -0.1f)
		{
//			this.GetComponent<SpriteRenderer> ().flipY = true;
			transform.localScale = negativeScale;
		}
		if (ControlFreak2.CF2Input.GetAxis("Horizontal") > 0.1f)
		{
//			this.GetComponent<SpriteRenderer> ().flipY = false;
			transform.localScale = positiveScale;
		}
	}

	private void SendBulletInfo(Vector2 bulletDirection, Quaternion bulletRotation) {
		DGTRemote.Instance.RequestSendBulletInfo (bulletDirection, bulletRotation);
	}
}
