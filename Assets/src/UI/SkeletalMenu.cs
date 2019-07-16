using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletalMenu : MonoBehaviour
{
    public GameEngine gameEngine;
    public PlayerScoring playerScoring;

    public void HandleButtonClicked()
    {
        Debug.Log("UI: Sending a messages to the Game Engine");
        gameEngine.UIRequestMessage();

        Debug.Log("UI: Sending a message to the Game Engine. Please get player score.");
        gameEngine.UIRequestPlayerScore();
    }
}
