using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;

public class LeaveRoom : MonoBehaviour
{

    [SerializeField]
    private GameObject LoserLabel;

    [SerializeField]
    private GameObject WinnerLabel;

    [SerializeField]
    private TMP_Text my_text;

    void Start()
    {
        Cursor.visible = true;
        my_text.text = "YOU are suck !!!";

        if (StateController.status == 1)
        {
            my_text.text = "YOU ARE CHAMPION !!! ";           
        }
        else if (StateController.status == 0)
        {
            /*PlayerPrefs.GetInt("Status")*/
            my_text.text = "GOT KILLED !!! ";
        }
        else 
        {
            
        }
        Cursor.visible = true;
        Screen.lockCursor = false;
        PlayerPrefs.DeleteAll();
    }



    public void GoBackLobby()
    {
        Destroy(KillAmount.instance.gameObject);
        Kill.killAmount = 0;
        SceneManager.LoadScene("Launcher");
    }

    public void Open() 
    {
        gameObject.SetActive(true);
    }
}
