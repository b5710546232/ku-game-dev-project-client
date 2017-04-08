using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightselectcontrol : MonoBehaviour {
    public GameObject gamecom;
    private Gameovercontrol gcon;
	// Use this for initialization
	void Start () {
        gcon = gamecom.GetComponent<Gameovercontrol>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnButtonIsClick()
    {
        //Debug.Log("Button is clicked");
        gcon.swip2();
        gcon.UpdateText();
    }
}
