using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    // Use this for initialization
    public DGTMainController dgtMainController;
    public Dictionary<string, PlayerController> PlayerControllerList = new Dictionary<string, PlayerController>();
    public bool connected;

    public GameObject Player;
    public GameObject PlayerController;

	public GameObject userController;

	public int owner_id;
    public List<GameObject> players;

    private static GameManager g_instance;
    private static GameObject gameObjectState;
	public static int count;

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
		DGTRemote gamestate = DGTRemote.GetInstance();
        gamestate = DGTRemote.GetInstance();


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
