using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedGameManager_Script : NetworkLobbyManager
{
    public List<Player> connectedPlayers;
    public List<int> holePar;
    public List<RoundStats> gameStats;

    [Header("Game Settings")]
    public bool allowCollisions = true;
    public int roundTimer = 120;
    public string ballShape = "Ball";
    public float gravityStrength = 1f;

    public void SetPort()
    {
        networkPort = 7777;
    }

    public void SetIpAddress()
    {

    }

    public void SetIpAddress(string address)
    {
        if (address == "")
            address = "localhost";

        networkAddress = address;
    }

    private void Update()
    {
        connectedPlayers.Clear();

        for(int i = 0; i < lobbySlots.Length; i++)
        {
            if (lobbySlots[i] != null)
            {
                LobbyPlayer_Script lP = lobbySlots[i].gameObject.GetComponent<LobbyPlayer_Script>();

                connectedPlayers.Add(new Player(lP.playerName, i, lP.ballColour));
            }
        }
    }
}