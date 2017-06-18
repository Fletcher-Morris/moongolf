using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer_Script : NetworkLobbyPlayer{

    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color ballColour;

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if(GameObject.Find("Color Selector Panel"))
        {
            ballColour = GameObject.Find("Color Selector Panel").GetComponent<ColourSelector_Script>().resultColor;
        }
    }
}