using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    
    static int MAX_PLAYERS = 2;
    int[] playerScores; 
    
    // Will probably want to expand this to an array and then object as the features are but in during later increments
    // for now we will assume they provide the player number, maybe in future the name is a good ID.
    // should player's number of free turns should be stored here maybe?

    // Start is called before the first frame update
    void Start()
    {
        int[] playerScores = new int[MAX_PLAYERS];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePlayers()
    {

        Debug.Log("Player Scoring, mama we made it");
    }

    public int GetPlayerScore(int playerNum)
    {
        //Debug.Log("Player Scoring: Got your message, sending you the player score now.");
        return playerScores[playerNum];
    }

    public void UpdatePlayerScore(int playerNum, int scoreAdjustment){
        //Debug.Log("Player Scoring: Player score is " + this.GetPlayerScores());
        playerScores[playerNum] += scoreAdjustment;
    }

    public int DoublePlayerScore(int playerNum)
    {
        playerScores[playerNum] *= 2;
        return playerScores[playerNum];
    }

    public void BankruptPlayer(int playerNum)
    {
        playerScores[playerNum] = 0;
    }

}
