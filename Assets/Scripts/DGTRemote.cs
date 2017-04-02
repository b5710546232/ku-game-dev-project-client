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
    public void recvAllPlayerInfo(Dictionary<int, ArrayList> players)
    {
        foreach (KeyValuePair<int, ArrayList> entry in players)
        {
            if (gameManager.PlayerControllerList.ContainsKey(entry.Key.ToString()))
            {

            }
            else
            {
                object[] info = entry.Value.ToArray();
                float posx = (float)(info[0]);
                float posy = (float)(info[1]);
                PlayerController p = gameManager.SpawnPlayer(posx, posy);
                gameManager.PlayerControllerList.Add(entry.Key.ToString(), p);

            }

        }


        List<string> entry_keys = new List<string>(gameManager.PlayerControllerList.Keys);
        foreach (string entry_key in entry_keys)
        {

            if (!players.ContainsKey(int.Parse(entry_key)))
            {
                Destroy(gameManager.PlayerControllerList[entry_key].gameObject);
                Debug.Log("hello it should destroy");
                gameManager.PlayerControllerList.Remove(entry_key);
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
}
