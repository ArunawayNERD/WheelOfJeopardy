using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Engine: Adding 200 points to the player");
        playerScoring.UpdatePlayerScore(200);
        Debug.Log("Game Engine: Removing 400 points from the player");
        playerScoring.UpdatePlayerScore(-400);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UIRequestMessage()
    {
        Debug.Log("Game Engine: Got your message UI");
    }

    public void UIRequestPlayerScore()
    {
        Debug.Log("Game Engine: Got your message, getting player score now.");
        score = playerScoring.GetPlayerScores();
        Debug.Log("Game Engine: Received player score: "+ score);
    }
}
