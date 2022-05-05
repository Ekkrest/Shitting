using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherVer2 : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings(); // �s����Photon���ݦ��A��
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby(); // ��s������A���ɡA�n�h�M��ж�(Lobby)
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
