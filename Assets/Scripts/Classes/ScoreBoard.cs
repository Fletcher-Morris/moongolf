using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreBoard
{
    public string boardName;
    public List<int> holePars;
    public List<PlayerScore> playerScores;
}

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int playerId;
    public List<int> holeScore;

    public PlayerScore()
    {
        playerName = "New Player";
        playerId = 0;
        holeScore = new List<int>();
    }

    public PlayerScore(string _playerName)
    {
        playerName = _playerName;
        playerId = 0;
        holeScore = new List<int>();
    }

    public PlayerScore(string _playerName, int _playerId)
    {
        playerName = _playerName;
        playerId = _playerId;
        holeScore = new List<int>();
    }
}