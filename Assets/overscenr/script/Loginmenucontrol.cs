using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loginmenucontrol : MonoBehaviour {
    public Image mainmenu;

    private Animation anim;
    // Use this for initialization
    void Start () {
        anim = mainmenu.GetComponent<Animation>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.Play("menuMovedown");
        }
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.Play("menuMoveup");
        }
    }
    public void onLogin()
    {

    } 
}
