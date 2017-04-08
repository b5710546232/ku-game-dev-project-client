using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gearselectcontrol : MonoBehaviour
{

    private Animation anim;
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.Stop();
            anim.Play("gear2move");

        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.Stop();
            anim.Play("gear1move");

        }
    }
}
