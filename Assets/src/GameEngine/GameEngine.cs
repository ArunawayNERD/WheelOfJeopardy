using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    public QuestionStore questionStore;

    public QuestionMenu questionMenu;

    public QuestionBoard board;

    void Start()
    {
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
