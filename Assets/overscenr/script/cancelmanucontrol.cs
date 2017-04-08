using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cancelmanucontrol : MonoBehaviour {
    public Image mainmenu;
    public Image cancelmenu;

    private Animation anim, ranim;
    // Use this for initialization
    void Start()
    {
        anim = mainmenu.GetComponent<Animation>();
        ranim = cancelmenu.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonIsClick()
    {
        RectTransform pos = mainmenu.rectTransform;
        Debug.Log("Button is clicked");
        ranim.Play("menuMovedown");
        //cancelmenu.rectTransform.position = pos.position;
        anim.Play("menuMoveup");


    }
}
