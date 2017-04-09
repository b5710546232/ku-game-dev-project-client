using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	public AudioClip bgm;
	public AudioSource player;


	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<AudioSource> ();
		PlayMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void PlayMusic(){
		player.clip = bgm;
		player.loop = true;
		player.Play ();
		
	}

	void OnLevelWasLoaded(int level){
		player.clip = bgm;
		player.loop = true;
		player.Play ();
	}
}
