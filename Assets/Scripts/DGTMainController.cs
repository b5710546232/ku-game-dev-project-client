using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DGTMainController : MonoBehaviour
{
	public Text m_chat;
	public InputField m_inputText;

	private static DGTMainController g_instance;

	private static GameObject gameObjectState;

	void Update ()
	{
		DGTRemote.GetInstance ().ProcessEvents (); 
	}
	
	// Use this for initialization
	void Start ()
	{
		Application.runInBackground = true;
		DontDestroyOnLoad(this);
		StartCoroutine (ConnectToServer ());
		
	}

	public static DGTMainController GetInstance() { 
		if(g_instance ==null){
			gameObjectState = new GameObject("DGTMainController");
			g_instance = gameObjectState.AddComponent<DGTMainController>();
			DontDestroyOnLoad(gameObjectState);
		}
		return g_instance;
	}
	
	public IEnumerator ConnectToServer ()
	{
		// string host = "139.59.127.218";
		 string host = "localhost";
//		string host = "192.168.1.3";
		int port = 3456;
		DGTPacket.Config pc = new DGTPacket.Config (host, port);
		DGTRemote.resetGameState ();
		DGTRemote gamestate = DGTRemote.GetInstance ();
		gamestate.Connect (pc.host, pc.port);
		gamestate.ProcessEvents ();
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 10; i++) {
			if (gamestate.Connected ()) {
				break;
			}
			if (gamestate.ConnectFailed ()) {
				break;
			}
			
			gamestate.ProcessEvents ();
			yield return new WaitForSeconds (i * 0.1f);
		}
		
		if (gamestate.Connected ()) {			
			Debug.Log ("Connected Finish");
			// send login
			gamestate.RequestLogin ();
			gamestate.mainController = this;
			// SceneManager.LoadScene("PlayScene");
			
			
		} else {
			yield return new WaitForSeconds (5f);
			Debug.Log ("Cannot connect");
		}
		// StartCoroutine(PingTest());
		yield break;
	}
	
	public IEnumerator PingTest ()
	{
		int i = 0;
		while (true) {
			DGTRemote.GetInstance ().TryPing(i);
			i++;
			yield return new WaitForSeconds(3);
		}
	}


	void OnApplicationQuit()
	{
		DGTRemote.GetInstance ().Disconnect();
	}

	public void sendChat()
	{
		if(DGTRemote.Instance.Connected())
		{
			DGTRemote.Instance.RequestSendChat(m_inputText.text);
		}
	}
}
