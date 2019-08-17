using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.src.UI;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    public QuestionStore questionStore;
    public QuestionMenu questionMenu;
    public TokenMenu tokenMenu;
    public QuestionBoard board;
    //public UseToken useToken;
    public TextMeshProUGUI currentRound;
    public TextMeshProUGUI spinsLeft;
    public TextMeshProUGUI currentPlayer;
    public List<Sector> sectorList;
    public TextMeshProUGUI p1score;
    public TextMeshProUGUI p2score;
    public TextMeshProUGUI p3score;
    public TextMeshProUGUI p1tokens;
    public TextMeshProUGUI p2tokens;
    public TextMeshProUGUI p3tokens;

    public Button spinWheelBtn;

    public Wheel wheel;
    private int currentRoundNum;
    private int spinsLeftInRound;

    private TokenUse reasonForToken;
    private int pendingTokenScore;

    private enum TokenUse {LoseTurn, Incorrect };

    //public Wheel Wheel { get => wheel; set => wheel = value; }

    public int CurrentRoundNum { get => currentRoundNum; set => currentRoundNum = value; }

    void Start()
    {
    	//Populate the round counter and spin counter
    	this.currentRound.SetText("1");
    	this.spinsLeft.SetText("50");
        this.currentRoundNum = 1;
        this.spinsLeftInRound = 50;

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

    //public void tokenUsed(bool used, string invoker, int qPts)
    //{
    //    if (used && invoker == "Lose turn")
    //    {
    //        // Player uses their token to spin wheel again.
    //        // TODO: Pull from Ben's implementation of spinning wheel to be able to spin wheel.
    //        playerScoring.ActivePlayer.UseToken();
    //    }
    //    else if (!used && invoker == "Lose turn")
    //    {
    //        // Tough luck, kid.
    //        this.NextTurn();
    //    }
    //    else if (used && invoker == "Question answered")
    //    {
    //        // Player is bad at this, but wants to keep trying. 
    //        // TODO: Pull from Ben's implementation of spinning wheel to be able to spin wheel.
    //        playerScoring.ActivePlayer.UseToken();
    //    }
    //    else if (!used && invoker == "Question answered")
    //    {
    //        // Player is bad at this and accepts that they are bad at this.
    //        playerScoring.UpdateActivePlayerScore(-qPts, currentRoundNum);
    //        this.NextTurn();
    //    }

    //}

    private void Update()
    {
        // Make sure the current round field is always correct.
        currentRound.SetText(currentRoundNum.ToString());
        // Make sure the current player field always has the current player's name.
        currentPlayer.SetText(playerScoring.ActivePlayer.Name);
        this.board.ReceiveQuestionAnswered(this.questionStore.getQuestionsAnswered());

        // adjust player scores & tokens
        this.p1score.SetText(playerScoring.GetPlayerScore(0).ToString());
        this.p2score.SetText(playerScoring.GetPlayerScore(1).ToString());
        this.p3score.SetText(playerScoring.GetPlayerScore(2).ToString());

        this.p1tokens.SetText(playerScoring.GetPlayerTokenCount(0).ToString());
        this.p2tokens.SetText(playerScoring.GetPlayerTokenCount(1).ToString());
        this.p3tokens.SetText(playerScoring.GetPlayerTokenCount(2).ToString());
    }

    public void CategorySelected(int categoryIndex)
    {
        string category = wheel.GetCategory(categoryIndex);
        this.CategorySelected(category);
    }

    public void CategorySelected(string category)
    {
        //string category = this.getQuestionCategories()[categoryIndex];
        //Debug.Log(category);

        int answered = this.questionStore.getQuestionsAnswered()[category];

        if (((200 * answered) + 200) < 1200)
        {
            Question nextQuestion = this.questionStore.getQuestion(category, (200 * answered) + 200);
            this.questionMenu.ReceiveQuestion(nextQuestion);
        }
    }

    public void spinWheel()
    {
        // Here randomly choose and notify what sector was landed on
        int sectIdx = UnityEngine.Random.Range(0, 11);  // 12 because 6 categories and 6 "other"- should probably not be hardcoded.
        Debug.Log("Next up: " + sectorList[sectIdx].Name);
        SectorLandedOn(sectorList[sectIdx]);

        // decrement spins counter
        this.spinsLeftInRound = this.spinsLeftInRound - 1;
        this.spinsLeft.SetText(this.spinsLeftInRound.ToString());
        
        //Place holder untill we have the whole loop
        //Question testQuestion = this.questionStore.getQuestion("Books", 200);
        //this.questionMenu.ReceiveQuestion(testQuestion);
    }

    public void handleTokenDecision(bool useToken)
    {
        if(!useToken)
        {
            //If they arent useing a token we need to let subtract points if we got here before of a wrong answer
            if (this.reasonForToken == TokenUse.Incorrect)
            {
                playerScoring.UpdateActivePlayerScore(this.pendingTokenScore, currentRoundNum);
            }

            this.NextTurn();
        }
        else
        {
            //If they used the token then we dont really need to do anything since we arent switching players. We just need to subtract a token
            this.playerScoring.ActivePlayer.UseToken();
        }
    }

    public void questionAnswered(int qPts, bool correct)
    {
        //For now print strings but when its built update the player store
        if(correct)
        {
            Debug.Log("Answer was correct");
            playerScoring.UpdateActivePlayerScore(qPts, currentRoundNum);
            this.NextTurn();
            
        }
        else
        {
            // Uh oh wrOng answer you get negative points (unless you use token).
            // TODO: implement token usage option in UI.
            Debug.Log("Answer was incorrect -- giving choice of using token -- if they have one.");
            if (playerScoring.ActivePlayer.GetTokenCount() > 0)
            {
                Debug.Log("They do");

                this.reasonForToken = TokenUse.Incorrect;
                this.pendingTokenScore = qPts;
                this.tokenMenu.UpdateVisability(true);

            } else
            {
                Debug.Log("They dont");
                playerScoring.UpdateActivePlayerScore(-qPts, currentRoundNum);
                this.NextTurn();
            }
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
        }
        else if (sector.Name == "Lose turn")
        {
            // TODO Target: Call to trigger UI element allowing choice of using token.
            // The player's response is a callback to GameEngine.tokenUsed()

            // For now, simulate action.
            Debug.Log("Landed on lose turn sector -- player given choice of using token to spin again -- if they have one");
            if (playerScoring.ActivePlayer.GetTokenCount() > 0)
            {
                this.reasonForToken = TokenUse.LoseTurn;
                this.tokenMenu.UpdateVisability(true);
            }


        }
        else if (sector.Name == "Free turn")
        {
            Debug.Log("Landed on free turn sector -- player given additional token");
            playerScoring.ActivePlayer.AddToken();
        }
        else if (sector.Name == "Bankrupt")
        {
            Debug.Log("Landed on bankrupt sector -- player loses all their points");
            playerScoring.UpdateActivePlayerScore(-playerScoring.GetActivePlayerScore(currentRoundNum), currentRoundNum);
            // Tough luck, kid.
            this.NextTurn();
        }
        else if (sector.Name == "Player's choice")
        {

           // this.CategorySelected(sector.Name);
        }
        else if (sector.Name == "Opponent's choice")
        {
            //this.CategorySelected(sector.Name);
        }
        else if (sector.Name == "Double your score")
        {
            playerScoring.UpdateActivePlayerScore(2*playerScoring.GetActivePlayerScore(currentRoundNum), currentRoundNum);
        }

        // Move to the next turn.

    }

    private void NextTurn()
    {
        playerScoring.NextPlayer();

        // TODO: CODE TO PROMPT SPINNER BUTTON
    }
}
