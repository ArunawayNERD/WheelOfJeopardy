using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    public QuestionStore questionStore;
    public QuestionMenu questionMenu;
    public QuestionBoard board;
    public Text currentRound;
    public Text spinsLeft;
    public Text currentPlayer;

    void Start()
    {
    	//Populate the round counter and spin counter
    	this.currentRound.text = "1";
    	this.spinsLeft.text = "50";

        //Place holder untill we have the whole loop
        Question testQuestion = this.questionStore.getQuestion("Books", 200);

        this.questionMenu.ReceiveQuestion(testQuestion);
    }

    private void Update()
    {
        this.board.ReceiveQuestionAnswered(this.questionStore.getQuestionsAnswered());
    }

    public void categorySelected(int categoryIndex)
    {
        string category = this.getQuestionCategories()[categoryIndex];
        int answered = this.questionStore.getQuestionsAnswered()[category];

        Question nextQuestion = this.questionStore.getQuestion(category, (200 * answered) + 200);
        this.questionMenu.ReceiveQuestion(nextQuestion);
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

    public string[] getQuestionCategories()
    {
        return questionStore.getCategories();
    }
}
