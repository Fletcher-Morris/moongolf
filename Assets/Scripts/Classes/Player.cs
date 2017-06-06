using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerId;
    public string playerName;
    public string steamId;
    public Color ballColor;

    public int remainingShots;
    public int score;

    public Player()
    {
        playerId = "";
        playerName = "New Player";
        steamId = "";
        ballColor = Color.blue;

        remainingShots = 10;
        score = 0;
    }
}