using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayerUiScript : MonoBehaviour
{
    public GameObject lobbyPlayerObject;
    public GameObject colourSelectorObject;
    public GameObject readyButtonObject;
    public GameObject colourButtonObject;

    public bool colourSelectorOpen = false;

    private void Start()
    {
        if (lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().isLocal)
        {
            lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().CmdSendName(lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().playerName); 
        }
    }

    private void Update()
    {
        if (!lobbyPlayerObject)
        {
            Destroy(gameObject);
        }
    }

    public void OpenColourSelector()
    {
        if (colourSelectorObject.activeInHierarchy)
        {
            CloseColourSelector();
        }
        else
        {
            if (lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().isLocal)
            {
                colourSelectorOpen = true;

                colourSelectorObject.GetComponent<ColourSelector_Script>().rSlider.GetComponent<Slider>().value = colourButtonObject.GetComponent<Image>().color.r;
                colourSelectorObject.GetComponent<ColourSelector_Script>().gSlider.GetComponent<Slider>().value = colourButtonObject.GetComponent<Image>().color.g;
                colourSelectorObject.GetComponent<ColourSelector_Script>().bSlider.GetComponent<Slider>().value = colourButtonObject.GetComponent<Image>().color.b;

                colourSelectorObject.SetActive(true);
                readyButtonObject.SetActive(false); 
            }
        }
    }

    public void CloseColourSelector()
    {
        colourSelectorOpen = false;

        lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().ballColour = colourButtonObject.GetComponent<Image>().color;
        colourSelectorObject.SetActive(false);
        readyButtonObject.SetActive(true);
        lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().CmdSendColour(colourSelectorObject.GetComponent<ColourSelector_Script>().resultColor);
        lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().CmdSendName(lobbyPlayerObject.GetComponent<LobbyPlayer_Script>().playerName);
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