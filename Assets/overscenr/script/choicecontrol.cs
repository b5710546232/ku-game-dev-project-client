using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choicecontrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 10f);
        if (this.gameObject.transform.position.x >= 20f)
        {
            Destroy(this.gameObject);
        }
    }
}
