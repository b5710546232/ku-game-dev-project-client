﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Use this for initialization
	public DGTMainController dgtMainController;
    public Dictionary<string, PlayerController> PlayerControllerList = new Dictionary<string, PlayerController>();

	// Dictionary containing all of the players
	public Dictionary<int, GameObject> players = new Dictionary<int, GameObject> ();

	public bool connected;

    public GameObject Player;
    public GameObject PlayerController;

	public GameObject userController;

	// Id of this game instance which is correspond with its remote on the server
	public int owner_id = -1;

    private static GameManager g_instance;
    private static GameObject gameObjectState;
	public static int count;

	// Explicitly chosen player's sprite
//	public Sprite playerSprite;
	// Explicitly chose player's prefab
	public GameObject clientPlayerPrefab;
    public GameObject otherPlayerPrefab;

	// Explicitly chosen gun's sprite
//	public Sprite gunSprite;

	// Explicity chose bullet's prefab
	public GameObject bulletPrefab;

    public GameObject bulletPool;

	public GameObject deadScreenPrefab;
	public GameObject deadScreen;

	public GameObject client;

    public static GameManager GetInstance()
    {
        if (g_instance == null)
        {
            gameObjectState = new GameObject("GameManager");
            g_instance = gameObjectState.AddComponent<GameManager>();
            DontDestroyOnLoad(gameObjectState);
        }
        return g_instance;
    }

    void Awake()
    {
        dgtMainController = DGTMainController.GetInstance();
		deadScreen = Instantiate (deadScreenPrefab);
		deadScreen.SetActive (false);
//		DGTRemote gamestate = DGTRemote.GetInstance();
//        gamestate = DGTRemote.GetInstance();
    }

    void Start()
    {
       
    }

    public void InitUserController(GameObject gameobj){
        gameobj.AddComponent(typeof(UserController));
    }

    public GameObject SpawnPlayer(float posx, float posy)
    {

        if (DGTRemote.GetInstance().Connected())
        {
           GameObject  player = Instantiate(Player,new Vector3(posx,posy,1),Quaternion.identity);
			count++;
			return player;
            
        }
		return null;

    }

    // Update is called once per frame
    void Update()
    {
		DGTRemote.GetInstance ().gameManager = this;

    }
}
