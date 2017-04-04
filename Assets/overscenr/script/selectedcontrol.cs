using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectedcontrol : MonoBehaviour {
    public GameObject gamecom;
    private Gameovercontrol gcon;
    private Animation anim;
    // Use this for initialization
    void Start()
    {
        gcon = gamecom.GetComponent<Gameovercontrol>();
        anim = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.Stop();
            
            //gcon.UpdateText();

        }
    }
    public void OnButtonIsClick()
    {
        //Debug.Log("Button is clicked");
        gcon.select();
    }
}
