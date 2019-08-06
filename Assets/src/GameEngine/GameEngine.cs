using Assets.src.PlayerScoring;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    public QuestionStore questionStore;
    public QuestionMenu questionMenu;
    public QuestionBoard board;
    public TextMeshProUGUI currentRound;
    public TextMeshProUGUI spinsLeft;
    public TextMeshProUGUI currentPlayer;
    public List<Sector> sectorList;
    public GameObject menuGraphics;
    public Button spinWheelBtn;

    private Wheel wheel;
    private int currentRoundNum;

    public Wheel Wheel { get => wheel; set => wheel = value; }

    public int CurrentRoundNum { get => currentRoundNum; set => currentRoundNum = value; }

    void Start()
    {
    	//Populate the round counter and spin counter
    	this.currentRound.SetText("1");
    	this.spinsLeft.SetText("50");
        this.currentRoundNum = 1;

        // Generate sector list
        sectorList = new List<Sector>();
        sectorList.Add(new Sector("Double your score", "Non-category"));
        sectorList.Add(new Sector("Opponent's choice", "Non-category"));
        sectorList.Add(new Sector("Player's choice", "Non-category"));
        sectorList.Add(new Sector("Bankrupt", "Non-category"));
        sectorList.Add(new Sector("Free turn", "Non-category"));
        sectorList.Add(new Sector("Lose turn", "Non-category"));
        // add categories to sector list
        string[] cats = this.getQuestionCategories();
        for (int i = 0; i < cats.Length; i++)
        {
            sectorList.Add(new Sector(cats[i], "Category"));
        }
    }

    private void Update()
    {
        // Make sure the current round field is always correct.
        currentRound.SetText(currentRoundNum.ToString());
        // Make sure the current player field always has the current player's name.
        currentPlayer.SetText(playerScoring.ActivePlayer.Name);
        this.board.ReceiveQuestionAnswered(this.questionStore.getQuestionsAnswered());
    }

    public void CategorySelected(string category)
    {
        //string category = this.getQuestionCategories()[categoryIndex];
        int answered = this.questionStore.getQuestionsAnswered()[category];

        Question nextQuestion = this.questionStore.getQuestion(category, (200 * answered) + 200);
        this.questionMenu.ReceiveQuestion(nextQuestion);
    }

    public void spinWheel()
    {
        // Here randomly choose and notify what sector was landed on
        int sectIdx = Random.Range(0, 11);  // 12 because 6 categories and 6 "other"- should probably not be hardcoded.
        SectorLandedOn(sectorList[sectIdx]);
        Debug.Log("Next up: " + sectorList[sectIdx].Name + " of type: " + sectorList[sectIdx].Type);

        // make button invisible
        //this.menuGraphics.SetActive(true);
        //this.spinWheelBtn.gameObject.SetActive(false);

        //Place holder untill we have the whole loop
        Question testQuestion = this.questionStore.getQuestion("Books", 200);
        this.questionMenu.ReceiveQuestion(testQuestion);
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
            // TODO implement token usage option in UI.
            playerScoring.UpdateActivePlayerScore(-qPts, currentRoundNum);
            Debug.Log("Answer was incorrect");
        }

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
            this.NextTurn();
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

        // Move to the next turn.
        //this.spinWheelBtn.gameObject.SetActive(true);


    }

    private void NextTurn()
    {
        playerScoring.NextPlayer();
        // TODO: CODE TO PROMPT SPINNER BUTTON
    }
}
