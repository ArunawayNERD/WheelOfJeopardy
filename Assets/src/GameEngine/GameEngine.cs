using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    public QuestionStore questionStore;

    public QuestionMenu questionMenu;

    void Start()
    {
        //Place holder untill we have the whole loop
        Question testQuestion = this.questionStore.getQuestion("Books", 200);

        this.questionMenu.ReceiveQuestion(testQuestion);        
    }

    public void InitializePlayers()
    {

        playerScoring.InitializePlayers();
    }

    public void questionAnswered(bool correct)
    {
        //For now print strings but when its built update the player store
        if(correct)
        {
            Debug.Log("Answer was correct");
        }
        else
        {
            Debug.Log("Answer was incorrect");
        }
    }
}
