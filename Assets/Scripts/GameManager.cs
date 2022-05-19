using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Prefab- ���a������")]
    public GameObject playerPrefab;

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("playerPrefab ��, �Цb Game Manager ���s�]�w",
                this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.LogFormat("�ڭ̱q {0} �ʺA�ͦ����a����",
                    SceneManagerHelper.ActiveSceneName);

                PhotonNetwork.Instantiate(this.playerPrefab.name,
                    new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.LogFormat("�����������J for {0}",
                    SceneManagerHelper.ActiveSceneName);
            }
        }
    }
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
        PhotonNetwork.LoadLevel("GameScene");
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
