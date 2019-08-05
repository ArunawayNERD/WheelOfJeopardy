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

    internal Player ActivePlayer { get => activePlayer; set => activePlayer = value; }

    // Start is called before the first frame update
    void Start()
    {
        // Eventually make this something users can input.
        Player pl1 = new Player { Name = "Chad", RoundOneScore = 0, RoundTwoScore = 0, Tokens = 5 };
        Player pl2 = new Player { Name = "Kyle", RoundOneScore = 0, RoundTwoScore = 0, Tokens = 5 };
        Player pl3 = new Player { Name = "Karen", RoundOneScore = 0, RoundTwoScore = 0, Tokens = 5 };
        players = new Player[] { pl1, pl2, pl3 };

        // Classic Chad move, always being the first player
        activePlayer = players[0];
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
}
