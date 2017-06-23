using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Steamworks;

public class LobbyPlayer_Script : NetworkLobbyPlayer{

    public string playerName;
    public Color ballColour;

    public GameObject lobbyPlayerUiPrefab;
    public GameObject myLobbyPlayerUi;

    public override void OnClientEnterLobby()
    {
        myLobbyPlayerUi = GameObject.Instantiate(lobbyPlayerUiPrefab, GameObject.Find("Connected Players Canvas").transform.GetChild(0));
        myLobbyPlayerUi.GetComponent<LobbyPlayerUiScript>().lobbyPlayerObject = gameObject;

        if (!isLocalPlayer)
            return;

        if (SteamManager.Initialized)
        {
            playerName = SteamFriends.GetPersonaName(); 
        }

        ballColour = Random.ColorHSV();
    }

    private void Update()
    {
        if (myLobbyPlayerUi)
        {
            if (readyToBegin)
            {
                myLobbyPlayerUi.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "READY";
            }
            else
            {
                myLobbyPlayerUi.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "NOT READY";
            } 
        }

        if (!isLocalPlayer)
            return;

        if (GameObject.Find("Color Selector Panel"))
        {
            ballColour = GameObject.Find("Color Selector Panel").GetComponent<ColourSelector_Script>().resultColor;
        }

        if(playerName == "")
            myLobbyPlayerUi.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "UNKNOWN PLAYER";
        else
        {
            myLobbyPlayerUi.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = playerName;
        }


        if (myLobbyPlayerUi)
        {
            myLobbyPlayerUi.transform.GetChild(0).GetComponent<Toggle>().interactable = true;
        }
    }
}