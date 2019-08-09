using Assets.src.PlayerScoring;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    //Will probably want to expand this to an array and then object as the features are but in during later increments
    private int playerScore = 0;
    private Player[] players;
    private Player activePlayer;
    private int activePlayerIndex;
    private int numPlayers;
    private List<string> pNames;

    public int ActivePlayerIndex { get => activePlayerIndex; set => activePlayerIndex = value; }
    public int NumPlayers { get => numPlayers; set => numPlayers = value; }
    internal Player[] Players { get => players; set => players = value; }
    internal Player ActivePlayer { get => activePlayer; set => activePlayer = value; }

    // Start is called before the first frame update
    void Start()
    {
        // Eventually make this something users can input.
        numPlayers = 3;
        Player pl1 = new Player { Name = "Chad", RoundOneScore = 0, RoundTwoScore = 0, Tokens = 0 };
        Player pl2 = new Player { Name = "Kyle", RoundOneScore = 0, RoundTwoScore = 0, Tokens = 0 };
        Player pl3 = new Player { Name = "Karen", RoundOneScore = 0, RoundTwoScore = 0, Tokens = 0 };
        Players = new Player[] { pl1, pl2, pl3 };
        pNames = new List<string>(new string[] {"Chad", "Kyle", "Karen" });

        // Classic Chad move, always being the first player
        activePlayerIndex = 0;
        activePlayer = players[activePlayerIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlayerScores()
    {
        //Debug.Log("Player Scoring: Got your message, sending you the player score now.");
        return playerScore;
    }

    public int GetActivePlayerScore(int round)
    {
        if (round == 1)
        {
            return activePlayer.RoundOneScore;
        }
        else
        {
            return activePlayer.RoundTwoScore;
        }
    }

    public List<string> GetPlayerNames()
    {
        return pNames;
    }

    public int GetPlayerScore(int pNum)
    {
        return Players[pNum].RoundOneScore;
    }

    public int GetPlayerTokenCount(int pNum)
    {
        return Players[pNum].Tokens;
    }

    public void UpdateActivePlayerScore(int scoreAdjustment, int round){
        if (round == 1)
        {
            activePlayer.RoundOneScore += scoreAdjustment;
        }
        else
        {
            activePlayer.RoundTwoScore += scoreAdjustment;
        }
        //Debug.Log("Player Scoring: Player score is " + this.GetPlayerScores());
    }

    public void NextPlayer()
    {
        activePlayerIndex = (activePlayerIndex + 1) % numPlayers;
        activePlayer = players[activePlayerIndex];
    }
}
