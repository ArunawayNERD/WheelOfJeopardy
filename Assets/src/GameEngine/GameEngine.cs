using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.src.UI;
using System.Threading;
using System.IO;

public class GameEngine : MonoBehaviour
{
    public PlayerScoring playerScoring;
    public QuestionStore questionStore;
    public QuestionMenu questionMenu;
    public TokenMenu tokenMenu;
    public QuestionBoard board;
    public Infobar infoBar;
    public PlayerStats playerStats;
    public AudioScript bankruptAudio;
    public AudioScript correctAudio;
    public AudioScript doubleAudio;
    public AudioScript winnerAudio;
    public AudioScript incorrectAudio;

    public List<Sector> sectorList;

    public Button spinWheelBtn;
    public GameObject arrow;
    public Wheel wheel;
    private int currentRoundNum;
    private int spinsLeftInRound;
    private int spins = 10;

    private TokenUse reasonForToken;
    private int pendingTokenScore;
    private bool enteredDataSrc;

    private enum TokenUse {LoseTurn, Incorrect, Timer};

    public int CurrentRoundNum { get => currentRoundNum; set => currentRoundNum = value; }

    void Start()
    {
    	//Populate the round counter and spin counter
        this.currentRoundNum = 1;
        this.spinsLeftInRound = spins;

        this.infoBar.updateRoundInfo(this.currentRoundNum, this.spinsLeftInRound);

        // Generate sector list
        sectorList = new List<Sector>();
        sectorList.Add(new Sector("Double your Score", "Non-category"));
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
            Debug.Log(cats[i]);
        }
    }

    void Update()
    {
        this.infoBar.updateRoundInfo(this.currentRoundNum, this.spinsLeftInRound);

        // Make sure the current player field always has the current player's name.
        this.infoBar.updatePlayerName(playerScoring.ActivePlayer.Name);


        //Send the board the question info to turn on and off the 
        this.board.ReceiveQuestionAnswered(this.questionStore.getQuestionsAnswered());

        // adjust player scores & tokens
        this.playerStats.updatePlayerInfo(this.playerScoring.Players[0],
                                          this.playerScoring.Players[1],
                                          this.playerScoring.Players[2]);
    }

    public void SetDataSrc(bool dataEntered)
    {
        
        this.enteredDataSrc = true;
        // Entering questions results in a reset to round 1.
        this.currentRoundNum = 1;
        bool questionsWritten = false;
        // Make sure the CSVs have time to be written to.
        while (!questionsWritten)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Assets\\Resources\\EnteredQuestionData1.csv"))
            {
                Debug.Log("Round 1 entered question data found");
                questionsWritten = true;
                this.questionStore.switchToEnteredData1();
                this.wheel.switchToEnteredData1();
            }
        }
        this.board.updateCategories();
    }

    public void CategorySelected(int categoryIndex)
    {
        string category = wheel.GetCategory(categoryIndex);
        this.CategorySelected(category);
    }

    public void CategorySelected(string category)
    {
        int answered = this.questionStore.getQuestionsAnswered()[category];

        if (answered < 5)
        {
            int scoreToGet = (answered + 1) * 200;

            if (this.currentRoundNum > 1)
            {
                scoreToGet *= 2;
            }

            Question nextQuestion = this.questionStore.getQuestion(category, scoreToGet);
            this.questionMenu.ReceiveQuestion(nextQuestion);
        }
    }

    public void handleTimerRunOut()
    {
        if (playerScoring.ActivePlayer.GetTokenCount() > 0)
        {

            this.reasonForToken = TokenUse.Timer;
            this.tokenMenu.UpdateVisability(true);

        }
        else
        {
            this.NextTurn();
        }
    }

    public void handleTokenDecision(bool useToken)
    {
        if(!useToken)
        {
            //If they arent useing a token we need to let subtract points if we got here before of a wrong answer
            if (this.reasonForToken == TokenUse.Incorrect)
            {
                playerScoring.UpdateActivePlayerScore(-this.pendingTokenScore, currentRoundNum);
            }

            this.NextTurn();
        }
        else
        {
            //If they used the token then we dont really need to do anything since we arent switching players. We just need to subtract a token
            this.playerScoring.ActivePlayer.UseToken();
        }

        this.CheckRound2();
    }

    public void questionAnswered(int qPts, bool correct)
    {
        //For now print strings but when its built update the player store
        if(correct)
        {
            Debug.Log("Answer was correct");
            this.correctAudio.PlayClip();
            playerScoring.UpdateActivePlayerScore(qPts, currentRoundNum);
            this.CheckRound2();
        }
        else
        {
            this.incorrectAudio.PlayClip();
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
                this.CheckRound2();
            }
        }

    }

    public string[] getQuestionCategories()
    {
        return questionStore.getCategories();
    }

    public void SectorLandedOn(Sector sector)
    {
        //The wheel spun so remove one from the count
        this.spinsLeftInRound = this.spinsLeftInRound - 1;

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
            else
            {
                this.NextTurn();
                this.CheckRound2();
            }


        }
        else if (sector.Name == "Free turn")
        {
            Debug.Log("Landed on free turn sector -- player given additional token");
            playerScoring.ActivePlayer.AddToken();
            //this.NextTurn();
            this.CheckRound2();
        }
        else if (sector.Name == "Bankrupt")
        {
            this.bankruptAudio.PlayClip();
            Debug.Log("Landed on bankrupt sector -- player loses all their points");
            playerScoring.UpdateActivePlayerScore(-playerScoring.GetActivePlayerScore(currentRoundNum), currentRoundNum);
            // Tough luck, kid.
            this.NextTurn();
            this.CheckRound2();
        }
        else if (sector.Name == "Player's choice")
        {

            // this.CategorySelected(sector.Name);
         
        }
        else if (sector.Name == "Opponent's choice")
        {
            //this.CategorySelected(sector.Name);
            
        }
        else if (sector.Name == "Double your Score")
        {
            this.doubleAudio.PlayClip();
            playerScoring.UpdateActivePlayerScore(playerScoring.GetActivePlayerScore(currentRoundNum), currentRoundNum);
            this.NextTurn();
            this.CheckRound2();
        }

    }

    private void NextTurn()
    {
        playerScoring.NextPlayer();

        // TODO: CODE TO PROMPT SPINNER BUTTONs
    }

    public void CheckRound2() {


        Dictionary<string, int> questionsAnswered = this.questionStore.getQuestionsAnswered();

        int totalAnswered = 0;
        foreach (int answered in questionsAnswered.Values)
        {
            totalAnswered += answered;
        }

        if (this.spinsLeftInRound < 1 || totalAnswered >= 30)
        {
            if (this.currentRoundNum == 1)
            {
                this.currentRoundNum = 2;
                this.spinsLeftInRound = spins;

                if (this.enteredDataSrc)
                {
                    this.questionStore.switchToEnteredData2();
                    this.wheel.switchToEnteredData2();
                }
                else
                {
                    this.questionStore.switchToRoundTwo();
                    this.wheel.switchToRoundTwo();
                }

                this.board.resetForRoundTwo();

            }
            else
            {
                this.wheel.gameObject.SetActive(false);
                this.arrow.gameObject.SetActive(false);
                this.spinWheelBtn.gameObject.SetActive(false);

                //who won?
                int player1Score = this.playerScoring.GetPlayerScore(0) + this.playerScoring.GetPlayerScoreRoundTwo(0);
                int player2Score = this.playerScoring.GetPlayerScore(1) + this.playerScoring.GetPlayerScoreRoundTwo(1);
                int player3Score = this.playerScoring.GetPlayerScore(2) + this.playerScoring.GetPlayerScoreRoundTwo(2);
                int ans = Math.Max(Math.Max(player1Score, player2Score), player3Score);
                String winnerIs;
                Debug.Log("max is " + Math.Max(Math.Max(player1Score, player2Score), player3Score));
                if (player1Score == ans)
                {
                    winnerIs = this.playerScoring.GetPlayerNames()[0];
                }
                else if (player2Score == ans)
                {
                    winnerIs = this.playerScoring.GetPlayerNames()[1];
                }
                else
                {
                    winnerIs = this.playerScoring.GetPlayerNames()[2];
                }

                this.winnerAudio.PlayClip();
                this.infoBar.winner.SetText("Winner is " + winnerIs + "!");
                this.infoBar.winner.gameObject.SetActive(true);
            }
        }
    }
}
