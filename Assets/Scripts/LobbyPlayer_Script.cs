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

    public bool isLocal;

    public GameObject lobbyPlayerUiPrefab;
    public GameObject myLobbyPlayerUi;

    public override void OnStartLocalPlayer()
    {
        isLocal = isLocalPlayer;

        if (SteamManager.Initialized)
        {
            playerName = SteamFriends.GetPersonaName();
        }

        ballColour = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
    }

    public override void OnClientEnterLobby()
    {
        myLobbyPlayerUi = GameObject.Instantiate(lobbyPlayerUiPrefab, GameObject.Find("Connected Players Canvas").transform.GetChild(0));
        myLobbyPlayerUi.GetComponent<LobbyPlayerUiScript>().lobbyPlayerObject = gameObject;
        if (!isLocalPlayer)
            return;

        CmdSendColour(ballColour);
        CmdSendName(playerName);
        Debug.Log("Send Name : " + playerName);
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

    [Command]
    public void CmdSendName(string _name)
    {
        RpcSendName(_name);
    }
    [ClientRpc]
    public void RpcSendName(string _name)
    {
        this.playerName = _name;
    }

    private void Update()
    {
        isLocal = isLocalPlayer;

        if (myLobbyPlayerUi)
        {
            if (readyToBegin)
            {
                myLobbyPlayerUi.GetComponent<LobbyPlayerUiScript>().readyButtonObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "READY";
            }
            else
            {
                myLobbyPlayerUi.GetComponent<LobbyPlayerUiScript>().readyButtonObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "NOT READY";
            }

            if (playerName == "")
                myLobbyPlayerUi.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = ":(";
            else
            {
                myLobbyPlayerUi.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = playerName;
            }

            myLobbyPlayerUi.transform.GetChild(2).gameObject.GetComponent<Image>().color = ballColour;
        }

        if (!isLocalPlayer)
            return;

        if (myLobbyPlayerUi)
        {
            myLobbyPlayerUi.transform.GetChild(1).GetComponent<Toggle>().interactable = true;

            if (myLobbyPlayerUi.GetComponent<LobbyPlayerUiScript>().colourSelectorOpen == false)
            {
                CmdSendColour(ballColour);
            }
        }
    }
}