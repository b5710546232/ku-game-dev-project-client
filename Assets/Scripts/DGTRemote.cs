using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DGTRemote : MonoBehaviour
{
    public DGTMainController mainController;
    public GameManager gameManager;

    private enum State
    {
        DISCONNECTED = 0,
        DISCONNECTING,
        CONNECTED,
        CONNECTING,
    };


    private State _State;
    private DGTPacket _Packet;

    ////////////////////////////////////////////////////////////////////////////////
    // Singleton Design Pattern.
    ////////////////////////////////////////////////////////////////////////////////

    private static GameObject gameObjectState;
    private static DGTRemote g_instance;
    public static DGTRemote GetInstance()
    {
        if (g_instance == null)
        {
            gameObjectState = new GameObject("DGTRemote");
            g_instance = gameObjectState.AddComponent<DGTRemote>();
            DontDestroyOnLoad(gameObjectState);
        }
        return g_instance;
    }
    public static void resetGameState()
    {

        Destroy(gameObjectState);
        g_instance = null;
    }
    public static DGTRemote Instance { get { return GetInstance(); } }

    ////////////////////////////////////////////////////////////////////////////////
    public void Connect(string host, int port)
    {
        if (_State != State.DISCONNECTED) return;

        _State = State.CONNECTING;
        _Packet.Connect(host, port);
    }

    public void Disconnect()
    {
        //		Debug.Log (" Disconnect : _State "+ _State);
        if (_State != State.CONNECTED) return;
        _State = State.DISCONNECTED;
        _Packet.Disconnect();
    }

    public void OnConnected()
    {
        //		Debug.Log (" Connected : _State "+ _State);
        _State = State.CONNECTED;
    }

    public void OnDisconnected()
    {
        if (_State != State.DISCONNECTED)
        {

        }
        _State = State.DISCONNECTED;
    }

    public void OnFailed()
    {
        if (_State != State.DISCONNECTED)
        {

        }
        _State = State.DISCONNECTED;
    }

    public bool Connected()
    {
        return _Packet.Connected && _State == State.CONNECTED;
    }

    public bool ConnectFailed()
    {
        return _Packet.Failed;
    }

    public void ProcessEvents()
    {

        _Packet.ProcessEvents();
    }

    void Awake()
    {
        _Packet = new DGTPacket(this);
        _State = State.DISCONNECTED;
        //test();
    }
    ////////////////////////////////////////////////////////////////////////////////
    public void RequestLogin()
    {
        _Packet.RequestLogin();
    }

    public void RequestSendChat(string msg)
    {
        _Packet.RequestSendChat(msg);
    }

    public void TryPing(int pingTime)
    {
        _Packet.RequestPing(pingTime);
    }
    public void RequestMovementPlayer(float x, float y)
    {
        _Packet.RequestMovementPlayer(x, y);
    }

	public void RequestInputAxes(float h, float v) {
		_Packet.RequestInputAxes (h, v);
	}

	public void RequestPlayersInfo()
	{
		_Packet.RequestPlayersInfo ();
	}

	public void RequestSendBulletInfo(Vector2 bulletDirection, Quaternion bulletRotation)
	{
		_Packet.RequestSendBulletInfo (bulletDirection, bulletRotation);
	}

	public void RequestProjectileHit(int id)
	{
		_Packet.RequestProjectileHit (id);
	}

	///////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////
	// Receiving side

    public void recvQuestion()
    {
        _Packet.RequestAnswer();
    }

    public void recvChat(string msg)
    {
        mainController.m_chat.text += msg + "\n";
    }

    public void recvNewPlayer(string msg)
    {
        Debug.Log("new player" + msg);

    }

    public void recvPlayerInfo(int id)
    {
        gameManager.owner_id = id;

    }

	public void RecvPlayerInfo (int id)
	{
		gameManager.owner_id = id;
	}

    public void RecvPlayerDisconnect(int id)
    {
        List<string> entry_keys = new List<string>(gameManager.PlayerControllerList.Keys);
        if (entry_keys.Contains(id.ToString()))
        {
            Debug.Log("hello it should destroy");
            Destroy(gameManager.PlayerControllerList[id.ToString()].gameObject);
            gameManager.PlayerControllerList.Remove(id.ToString());
        }



        // Destroy(gameManager.PlayerControllerList[entry_key].gameObject);
        // Debug.Log("hello it should destroy");
        // gameManager.PlayerControllerList.Remove(entry_key);
        // foreach (string entry_key in entry_keys)
        // {

        //     if (!players.ContainsKey(int.Parse(entry_key)))
        //     {
        //         // Destroy(gameManager.PlayerControllerList[entry_key].gameObject);
        //         // Debug.Log("hello it should destroy");
        //         // gameManager.PlayerControllerList.Remove(entry_key);
        //     }
        // }

    }
    public void recvAllPlayerInfo(Dictionary<int, ArrayList> players)
    {
        foreach (KeyValuePair<int, ArrayList> entry in players)
        {
            if (gameManager.PlayerControllerList.ContainsKey(entry.Key.ToString()))
            {
                object[] info = entry.Value.ToArray();
                float posx = (float)(info[0]);
                float posy = (float)(info[1]);
                gameManager.PlayerControllerList[entry.Key.ToString()].gameObject.transform.position = new Vector2(posx, posy);

            }
            else
            {
                object[] info = entry.Value.ToArray();
                float posx = (float)(info[0]);
                float posy = (float)(info[1]);
                PlayerController p = gameManager.SpawnPlayer(posx, posy).GetComponent<PlayerController>();
                gameManager.PlayerControllerList.Add(entry.Key.ToString(), p);


                if (gameManager.owner_id == entry.Key)
                {
                    Debug.LogError(gameManager.owner_id + " : " + entry.Key);
                    gameManager.InitUserController(p.gameObject);
                }


            }

        }


        List<string> entry_keys = new List<string>(gameManager.PlayerControllerList.Keys);
        foreach (string entry_key in entry_keys)
        {

            if (!players.ContainsKey(int.Parse(entry_key)))
            {
                // Destroy(gameManager.PlayerControllerList[entry_key].gameObject);
                // Debug.Log("hello it should destroy");
                // gameManager.PlayerControllerList.Remove(entry_key);
            }
        }
        // foreach(KeyValuePair<string, PlayerController> entry in gameManager.PlayerControllerList){
        // 	if(!players.ContainsKey(int.Parse(entry.Key))){
        // 		Destroy(entry.Value.gameObject);
        // 		Debug.Log("hello it should destroy");
        // 		gameManager.PlayerControllerList.Remove(entry.Key);
        // 	}
        // }

    }

	public void RecvPlayersInfo(Dictionary<int, ArrayList> playersInfo)
	{
		GameObject player;
		foreach (KeyValuePair<int, ArrayList> playerInfo in playersInfo) {
			Vector2 position = new Vector2 ((float)playerInfo.Value [0], (float)playerInfo.Value [1]);
//			bool shouldInterpolate = false;
			if (gameManager.players.ContainsKey (playerInfo.Key)) {
				player = gameManager.players [playerInfo.Key];
				player.SetActive (true);
				if (gameManager.owner_id == playerInfo.Key) {
					UserController uc = player.GetComponent<UserController> ();
					uc.serverPosition = position;
//					uc.hasPosition = true;
//					shouldInterpolate = uc.shouldInterpolate; 
//					uc.startInterpolate ();
				} else {
					PlayerController pc = player.GetComponent<PlayerController> ();
					pc.serverPosition = position;
//					pc.hasPosition = true;
//					shouldInterpolate = pc.shouldInterpolate;
//					pc.startInterpolate ();
					//pc.startInterpolate();
				}
			} else {
//				player = new GameObject ();
				player = Instantiate (gameManager.playerPrefab);
				if (gameManager.owner_id == playerInfo.Key) {
					player.GetComponentInChildren<GunController> ().isClient = true;
					player.name = "Client player#" + gameManager.owner_id.ToString();
					UserController uc = player.AddComponent<UserController> ();
					uc.serverPosition = position;
					uc.id = playerInfo.Key;
//					shouldInterpolate = uc.shouldInterpolate;
				} else {
					player.name = "Other player#" + playerInfo.Key.ToString();
					PlayerController pc = player.AddComponent<PlayerController> ();
					pc.serverPosition = position;
//					shouldInterpolate = pc.shouldInterpolate;
					pc.id = playerInfo.Key;
				}
//				SpriteRenderer sr = player.AddComponent<SpriteRenderer> ();
//				sr.sprite = gameManager.playerSprite;
				gameManager.players.Add (playerInfo.Key, player);
			}
//			Debug.Log ("Update player's position");

			UserController userController = player.GetComponent<UserController> ();
			PlayerController playerController = player.GetComponent<PlayerController> ();


			if (userController) {
				userController.PlayAnimate (player.transform.position.x - position.x , player.transform.position.y - position.y);
			}
			if (playerController) {
				playerController.PlayAnimate  (player.transform.position.x - position.x , player.transform.position.y - position.y);
			}



			player.transform.position = position;

//			if (!shouldInterpolate) {
//				player.transform.position = position;
//			}
//			Debug.Log (string.Format ("Update position of #{0} from {1},{2} to {3},{4}"
//				,playerInfo.Key,player.transform.position.x,player.transform.position.y,position.x, position.y));
//			player.transform.position = position;
		}
	}

	public void RecvRemovePlayer(int id) {
//		string _id = id.ToString ();
//		PlayerController otherPlayer;
//		GameManager.GetInstance ().PlayerControllerList.TryGetValue (_id, out otherPlayer);
		GameObject otherPlayer;
		gameManager.players.TryGetValue (id, out otherPlayer);
		if (otherPlayer != null) {
			Debug.Log ("Bye bye player#" + id);
			otherPlayer.SetActive (false);
//			Destroy (otherPlayer);
//			gameManager.players.Remove (id);
		}
	}

	public void RecvBulletInfo(int id, Vector2 direction)
	{
		if (gameManager.players.ContainsKey (id)) {
			Debug.Log (string.Format ("Player#{0} shot a bullet", id));
			GameObject player = gameManager.players [id];
			GunController gunCtrl = player.GetComponentInChildren<GunController> ();
			GameObject bulletHole = gunCtrl.bulletHole;

			Quaternion bulletRotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 180);
			//Make a bullet or take from bullet pool
			//Should take a bullet from client's pool
			// GameObject bullet = Instantiate(gameManager.bulletPrefab, bulletHole.transform.position, bulletRotation);
            GameObject bullet = gameManager.bulletPool.GetComponent<BulletPoolController>()
                            .init(bulletHole.transform.position,bulletRotation);
            
			bullet.GetComponent<BulletController> ().id = id;
			bullet.GetComponent<BulletController> ().owner = gameManager.players [id];
			bullet.GetComponent<Rigidbody2D> ().velocity = direction * gunCtrl.bulletSpeed;
		}
	}

}
