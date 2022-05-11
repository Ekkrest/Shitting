using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public override void OnLeftRoom()
    {
        // ���a���}�C���Ǯ�, ��L�a�^��C�����J�f
        SceneManager.LoadScene(0);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("�ڤ��O Master Client, �������J�������ʧ@");
        }
        Debug.LogFormat("���J{0}�H������",
            PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("GameScene" +
            PhotonNetwork.CurrentRoom.PlayerCount);
    }
    // Update is called once per frame

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("�ڬO Master Client ��? {0}",
                PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("{0} ���}�C����", otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("�ڬO Master Client ��? {0}",
                PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }
}
