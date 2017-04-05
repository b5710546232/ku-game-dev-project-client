using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject bulletHole;
	public GameObject bulletPrefab;

	public float bulletSpeed = 2f;
	public float shootInterval = 1f;
	public float currentInterval = 0f;
	// Use this for initialization
	void Start () {
//		SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer> ();
//		sr.sprite = GameManager.GetInstance ().gunSprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentInterval >= shootInterval) {
			Shoot ();
		}
		currentInterval += Time.deltaTime;
	}

	void Shoot() {
		if (Input.GetMouseButton (0)) {
			Vector2 target = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
			Vector2 direction = (target - (Vector2)bulletHole.transform.position);
			direction.Normalize ();
			Quaternion bulletRotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 180);
			GameObject bullet = Instantiate (bulletPrefab, bulletHole.transform.position, bulletRotation);
			bullet.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
			currentInterval = 0f;
		}
	}
}
