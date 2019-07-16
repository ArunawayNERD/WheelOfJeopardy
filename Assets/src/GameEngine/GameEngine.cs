using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring; 
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
}
