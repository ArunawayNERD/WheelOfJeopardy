﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    //Will probably want to expand this to an array and then object as the features are but in during later increments
    private int playerScore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlayerScores()
    {
        Debug.Log("Player Scoring: Got your message, sending you player score now.");
        return playerScore;
    }

    public void UpdatePlayerScore(int scoreAdjustment){
        playerScore += scoreAdjustment;
        Debug.Log("Player Scoring: Player score is " + this.GetPlayerScores());
    }
}