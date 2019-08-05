﻿using Assets.src.PlayerScoring;
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

    private Wheel wheel;
    private int currentRoundNum;

    public Wheel Wheel { get => wheel; set => wheel = value; }

    public int CurrentRoundNum { get => currentRoundNum; set => currentRoundNum = value; }

    void Start()
    {
    	//Populate the round counter and spin counter
    	this.currentRound.text = "1";
    	this.spinsLeft.text = "50";
        this.currentRoundNum = 1;

        //Place holder untill we have the whole loop
        Question testQuestion = this.questionStore.getQuestion("Books", 200);

        this.questionMenu.ReceiveQuestion(testQuestion);
    }

    private void Update()
    {
        // Make sure the current round field is always correct.
        currentRound.text = currentRound.ToString();
        // Make sure the current player field always has the current player's name.
        currentPlayer.text = playerScoring.ActivePlayer.Name;
        this.board.ReceiveQuestionAnswered(this.questionStore.getQuestionsAnswered());
    }

    public void CategorySelected(string category)
    {
        //string category = this.getQuestionCategories()[categoryIndex];
        int answered = this.questionStore.getQuestionsAnswered()[category];

        Question nextQuestion = this.questionStore.getQuestion(category, (200 * answered) + 200);
        this.questionMenu.ReceiveQuestion(nextQuestion);
    }

    public void questionAnswered(int qPts, bool correct)
    {
        //For now print strings but when its built update the player store
        if(correct)
        {
            Debug.Log("Answer was correct");

            playerScoring.UpdateActivePlayerScore(qPts, currentRoundNum);
            
        }
        else
        {
            // Uh oh wrOng answer you get negative points.
            playerScoring.UpdateActivePlayerScore(-qPts, currentRoundNum);
            Debug.Log("Answer was incorrect");
        }

        // Switch players.

    }

    public string[] getQuestionCategories()
    {
        return questionStore.getCategories();
    }

    public void SectorLandedOn(Sector sector)
    {
        if (sector.Type == "Category")
        {
            this.CategorySelected(sector.Name);
        }
        else if (sector.Name == "Lose turn")
        {

        }
        else if (sector.Name == "Free turn")
        {

        }
        else if (sector.Name == "Bankrupt")
        {

        }
        else if (sector.Name == "Player's choice")
        {

        }
        else if (sector.Name == "Opponent's choice")
        {

        }
        else if (sector.Name == "Double your score")
        {

        }
    }
}
