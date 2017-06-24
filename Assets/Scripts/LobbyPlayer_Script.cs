using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Steamworks;

public class LobbyPlayer_Script : NetworkLobbyPlayer{

    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color ballColour;

    public GameObject lobbyPlayerUiPrefab;
    public GameObject myLobbyPlayerUi;

    public override void OnClientEnterLobby()
    {
        if (SteamManager.Initialized)
        {
            playerName = SteamFriends.GetPersonaName();
        }

        myLobbyPlayerUi = GameObject.Instantiate(lobbyPlayerUiPrefab, GameObject.Find("Connected Players Canvas").transform.GetChild(0));
        myLobbyPlayerUi.GetComponent<LobbyPlayerUiScript>().lobbyPlayerObject = gameObject;

        ballColour = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);

        if (!isLocalPlayer)
            return;

        myLobbyPlayerUi.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = playerName;
    }

    [Command]
    public void CmdSendColour(Color _col)
    {
        RpcSendColour(_col);
    }
    [ClientRpc]
    public void RpcSendColour(Color _col)
    {
        this.ballColour = _col;
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

            if (playerName == "")
                myLobbyPlayerUi.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "...";
            else
            {
                myLobbyPlayerUi.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = playerName;
            }

            myLobbyPlayerUi.transform.GetChild(1).gameObject.GetComponent<Image>().color = ballColour;
        }

        if (!isLocalPlayer)
            return;

        if (GameObject.Find("Color Selector Panel"))
        {
            //ballColour = GameObject.Find("Color Selector Panel").GetComponent<ColourSelector_Script>().resultColor;
        }


        if (myLobbyPlayerUi)
        {
            myLobbyPlayerUi.transform.GetChild(0).GetComponent<Toggle>().interactable = true;
        }

        CmdSendColour(ballColour);
    }
}