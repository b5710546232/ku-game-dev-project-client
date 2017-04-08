using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisBMcontrol : MonoBehaviour {
    public Image mainmenu;
    public Image regismenu;
    public Button conf;
    public InputField usern, passw, cpassw;

    private Animation anim, ranim;
    // Use this for initialization
    void Start () {
        anim = mainmenu.GetComponent<Animation>();
        ranim = regismenu.GetComponent<Animation>();
        Button cb = conf.GetComponent<Button>();
        cb.onClick.AddListener(delegate ()
        {
            sendData();
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnButtonIsClick()
    {
        RectTransform pos = mainmenu.rectTransform;
        
        Debug.Log("Button is clicked");
        anim.Play("menuMovedown");
        //regismenu.rectTransform.position = pos.position;
        ranim.Play("menuMoveup");


    }
    void sendData()
    {
        Debug.Log("Button is clicked");
    }
}
