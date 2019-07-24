using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletalMenu : MonoBehaviour
{
    public GameEngine gameEngine;
  

    public void HandleButtonClicked()
    {
        Debug.Log("UI: Sending a messages to the Game Engine");
        gameEngine.UIRequestMessage();
    }
}
