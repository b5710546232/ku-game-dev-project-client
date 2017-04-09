using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logincontrol : MonoBehaviour {
    public GameObject player,bullet;

    private Vector2[] move = new Vector2[3];
    private int npoint = 0;
    private bool shoot = true;
    // Use this for initialization
    void Start () {
        move[0] = new Vector2(-6.5f, 0.18f);
        move[1] = new Vector2(-6.5f, -1.88f);
        move[2] = new Vector2(-6.5f, -3.86f);
        moveplayer();
    }
	
	// Update is called once per frame
	void Update () {
		if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.Space)&&shoot){
            Vector2 posi = player.transform.position;
            posi.x += 2f;
            Instantiate(bullet, posi, Quaternion.identity);
        }
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.LeftArrow)) {
            shoot = false;
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.RightArrow))
        {
            shoot = true;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (npoint > 0)
            {
                npoint -= 1;
                moveplayer();
            }
             
        }
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (npoint < 2)
            {
                npoint += 1;
                moveplayer();
            }

        }


    }
    void moveplayer()
    {
        player.transform.position = move[npoint];
    }


}
