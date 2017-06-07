using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedGameManager_Script : NetworkLobbyManager
{
    public List<Player> connectedPlayers;

    [Header("Game Settings")]
    public bool allowCollisions = true;
    public int roundTimer = 120;
    public string ballShape = "Ball";

    public void StartGame()
    {

    }
}