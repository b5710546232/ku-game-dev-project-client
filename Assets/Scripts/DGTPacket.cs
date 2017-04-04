using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class DGTPacket : PacketManager
{
    public class Config
    {
        public string host;
        public int port;

        public Config(string h, int p)
        {
            host = h;
            port = p;
        }
    };

    private enum PacketId
    {
        CS_LOGIN = 10001,
        CS_PING = 10002,
        CS_ANSWER = 10003,
        CS_CHAT = 10004,
        CS_MOVE_PLAYER = 10005,

        SC_LOGGED_IN = 20001,
        SC_PING_SUCCESS = 20002,
        SC_QUESTION = 20003,
        SC_CHAT = 20004,
        SC_NEW_PLAYER = 20005,
        SC_ALL_PLAYERS_INFO = 20006,
        SC_PLAYER_INFO =20007,
        SC_PLAYER_DISCONNECT =20008,

    }

    private DGTRemote _remote;

    public DGTPacket(DGTRemote remote) : base()
    {
        _remote = remote;

        PacketMapper();
    }

    protected override void OnConnected()
    {
        _remote.OnConnected();
    }

    protected override void OnDisconnected()
    {
        _remote.OnDisconnected();
    }

    protected override void OnFailed()
    {
        _remote.OnFailed();
    }


    #region PacketMapper
    private void PacketMapper()
    {
        _Mapper[(int)PacketId.SC_LOGGED_IN] = RecvLogin;
        _Mapper[(int)PacketId.SC_PING_SUCCESS] = RecvPingSuccess;
        _Mapper[(int)PacketId.SC_QUESTION] = RecvQuestion;
        _Mapper[(int)PacketId.SC_CHAT] = RecvChat;
        _Mapper[(int)PacketId.SC_NEW_PLAYER] = RecvNewPlayer;
        _Mapper[(int)PacketId.SC_ALL_PLAYERS_INFO] = RecvAllPlayersInfo;
        _Mapper[(int)PacketId.SC_PLAYER_INFO] = RecvPlayerInfo;
        _Mapper[(int)PacketId.SC_PLAYER_DISCONNECT] = RecvPlayerDisconnect;
    }
    #endregion

    #region send to server
    public void RequestLogin()
    {
        PacketWriter pw = BeginSend((int)PacketId.CS_LOGIN);
        EndSend();
    }

    public void RequestPing(int pingTime)
    {
        PacketWriter pw = BeginSend((int)PacketId.CS_PING);
        pw.WriteInt8(pingTime);
        EndSend();
    }

    public void RequestAnswer()
    {
        Debug.Log("RequestAnswer");
        PacketWriter pw = BeginSend((int)PacketId.CS_ANSWER);
        EndSend();
    }

    public void RequestSendChat(string msg)
    {
        PacketWriter pw = BeginSend((int)PacketId.CS_CHAT);

        pw.WriteString(msg);

        EndSend();

    }

     public void RequestMovementPlayer(float x, float y)
    {
        PacketWriter pw = BeginSend((int)PacketId.CS_MOVE_PLAYER);
        // Debug.Log("write"+x+y);
        x = (float)(double)(x);
        y = (float)(double)(y);
        pw.WriteFloat(x);
        pw.WriteFloat(y);

        EndSend();

    }
    #endregion

    #region receive from server	
    private void RecvLogin(int packet_id, PacketReader pr)
    {
        Debug.Log("RecvLogin()");
    }

    private void RecvPingSuccess(int packet_id, PacketReader pr)
    {
        int pingTime = pr.ReadUInt8();
        Debug.Log("ping : " + pingTime);
    }

    private void RecvQuestion(int packet_id, PacketReader pr)
    {
        Debug.Log("RecvQuestion");
        DGTRemote.Instance.recvQuestion();
    }

    private void RecvChat(int packet_id, PacketReader pr)
    {
        string msg = pr.ReadString();
        Debug.Log("RecvChat" + msg);

        DGTRemote.Instance.recvChat(msg);
    }

    private void RecvNewPlayer(int packet_id, PacketReader pr)
    {
        string msg = pr.ReadString();
        Debug.Log("RecvNewPosition" + msg);
        
        DGTRemote.Instance.recvNewPlayer(msg);
    }

    private void RecvPlayerInfo(int packet_id, PacketReader pr){
        int own_in = pr.ReadUInt32();
        
        Debug.Log("RecvPlayerInfo ID : " + own_in);
        DGTRemote.Instance.recvPlayerInfo(own_in);
    }

    private void RecvAllPlayersInfo(int packet_id, PacketReader pr)
    {
        int playerLength = pr.ReadUInt8();
        Dictionary<int, ArrayList> players = new Dictionary<int, ArrayList>();
        // Debug.Log("PlayerLenght"+playerLength);
        for(int i = 0;i<playerLength;i++){
            int id = pr.ReadUInt32();
            float POSX = float.Parse(pr.ReadString());
            float POSY = float.Parse(pr.ReadString());
            
            
            ArrayList info = new ArrayList();
            info.Add(POSX);
            info.Add(POSY);
            // Debug.Log("RecvNewPosition");
            
            // Debug.Log("ID"+id);
            // Debug.Log("POSX"+POSX);
            // Debug.Log("POSY"+POSY);
            players.Add(id,info);
            DGTRemote.Instance.recvAllPlayerInfo(players);
        }
        
        
    }
    private void RecvPlayerDisconnect(int packet_id, PacketReader pr){
            int playerId = pr.ReadUInt32();
            DGTRemote.Instance.RecvPlayerDisconnect(playerId);
    }
    
    #endregion


}
