using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayerUiScript : MonoBehaviour
{
    public GameObject lobbyPlayerObject;

    private void Update()
    {
        if (!lobbyPlayerObject)
        {
            Destroy(gameObject);
        }
    }

    public void ToggleReady(Toggle thisToggle)
    {
        if (thisToggle.isOn)
        {
            ReadyUp();
        }
        else
        {
            ReadyDown();
        }
    }

    public void ReadyUp()
    {
        lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().SendReadyToBeginMessage();
    }

    public void ReadyDown()
    {
        lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().SendNotReadyToBeginMessage();
    }
}