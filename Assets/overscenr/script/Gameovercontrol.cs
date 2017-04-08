using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameovercontrol : MonoBehaviour {
    public Text a, b, c;
    public Image image;

    private Animation anim;
    private List<string> textcollect ;
    // Use this for initialization
    void Start () {
        anim = image.GetComponent<Animation>();
        textcollect = new List<string>();
        textcollect.Add("Retry");
        textcollect.Add("New Game");
        textcollect.Add("Exit");
        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            swip();
            //anim["gear2move"].speed = 1.0f;
            //anim.Play("gear2move");
            UpdateText();

        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            swip2();
            //anim["gear2move"].speed = -1.0f;
            //anim.Play("gear1move");
            UpdateText();

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            select();

        }
    }
    public void swip()
    {
        string zero= textcollect[0];
        for(int i = 1; i < textcollect.Count; i++)
        {
            textcollect[i - 1] = textcollect[i];
        }
        textcollect[textcollect.Count - 1] = zero;
    }
    public void swip2()
    {
        string zero = textcollect[textcollect.Count - 1];
        for (int i = textcollect.Count-1; i > 0; i--)
        {
            textcollect[i] = textcollect[i-1];
        }
        textcollect[0] = zero;
    }
    public void UpdateText()
    {
        float i = Time.time;
        a.text = textcollect[0];
        b.text = textcollect[1];
        c.text = textcollect[textcollect.Count - 1];
    }
    public void select()
    {
        Debug.Log(textcollect[0]+" Selected");
        if (textcollect[0].Equals("Exit")){
            SceneManager.LoadScene("Manuscene");
        }
        if (textcollect[0].Equals("Retry"))
        {
            SceneManager.LoadScene("GamOver");
        }
    }
}
