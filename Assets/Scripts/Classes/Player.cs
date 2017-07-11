using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerName;
    public int playerId;
    public Color ballColor;

    public Player()
    {
        playerName = "";
        playerId = 0;
        ballColor = Color.blue;
    }

    public Player(string _name)
    {
        playerName = _name;
        playerId = 0;
        ballColor = Color.blue;
    }

    public Player(int _id)
    {
        playerName = "";
        playerId = _id;
        ballColor = Color.blue;
    }

    public Player(string _name, int _id)
    {
        playerName = _name;
        playerId = _id;
        ballColor = Color.blue;
    }

    public Player(string _name, int _id, Color _ballColour)
    {
        playerName = _name;
        playerId = _id;
        ballColor = _ballColour;
    }
}