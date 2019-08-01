using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameEngine gameEngine;

    public void OnClick()
    {
        gameEngine.InitializePlayers();
        Debug.Log("Mama we made it.");
        GameObject.Find("Canvas/StartMenu/StartText").SetActive(false);
        GameObject.Find("Canvas/StartMenu/PlayerBox").SetActive(false);
        var q = GameObject.Find("Canvas/StartMenu/PlayerBox").GetComponent<Text>();
        q.text = "Tada!";
    }
    // Start is called before the first frame update
    void Start()
    {
        gameEngine.InitializePlayers();
        Debug.Log("Mama we made it.");
        GameObject.Find("Canvas/StartMenu/StartText").SetActive(false);
        GameObject.Find("Canvas/StartMenu/PlayerBox").SetActive(false);
        var q = GameObject.Find("Canvas/StartMenu/PlayerBox").GetComponent<Text>();
        q.text = "Tada!";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
