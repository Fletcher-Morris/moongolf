using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager_Script : NetworkBehaviour
{
    public NetworkedGameManager_Script nm;


    public ScoreBoard scoreBoard;

    private void Start()
    {
        nm = GameObject.Find("NM").GetComponent<NetworkedGameManager_Script>();

        for (int i = 0; i < nm.connectedPlayers.Count; i++)
        {
            scoreBoard.playerScores.Add(new PlayerScore(nm.connectedPlayers[i].playerName, i));
            Debug.Log("Added " + nm.connectedPlayers[i].playerName + " To Score Board.");
        }
    }
}