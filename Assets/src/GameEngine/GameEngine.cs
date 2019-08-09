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
	private int currentRoundNum;
	private Wheel wheel;

	public GameObject wheelToSpin;
    public bool spinning = false;
    public Button done;
    public float speed = 0; 

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
    	Rotate();
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
    /*
    public void spinWheel()
    {
        // Here randomly choose and notify what sector was landed on
        int sectIdx = Random.Range(0, 11);  // 12 because 6 categories and 6 "other"- should probably not be hardcoded.
        Debug.Log("Next up: " + sectorList[sectIdx].Name + " of type: " + sectorList[sectIdx].Type);
        SectorLandedOn(sectorList[sectIdx]);

        //Place holder untill we have the whole loop
        //Question testQuestion = this.questionStore.getQuestion("Books", 200);
        //this.questionMenu.ReceiveQuestion(testQuestion);
    }
    */

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
            Debug.Log("here");
            Question testQuestion = this.questionStore.getQuestion(sector.Name, 200);
            this.questionMenu.ReceiveQuestion(testQuestion);
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
        questionMenu.ResetMenu();

    }

    private void NextTurn()
    {
        //playerScoring.NextPlayer(sectorList.Find());
        // TODO: CODE TO PROMPT SPINNER BUTTON
    }
    
    //how to tell what the wheel stopped on
    void OnCollisionEnter2D(Collision2D col)
    {
 		//Debug.Log(col.gameObject.GetComponent<Text>().text);
 		for (int i = 0; i < sectorList.Count; i++) {
 			if (sectorList[i].name == col.gameObject.GetComponent<Text>().text) {
 				Debug.Log(sectorList[i].name + sectorList[i].type);
 				SectorLandedOn(sectorList[i]);
 				break;
 			}
 		}
 		//Debug.Log("answer " + sectorList.Find(x => x.name == col.gameObject.GetComponent<Text>().text));
 		//SectorLandedOn(sectorList.Find(col.gameObject.GetComponent<Text>().text));
    }
    void Rotate() {
    	wheelToSpin.transform.Rotate(0,0,-speed*Time.deltaTime);
    	if (speed > 0) {
    		Stop();
    	}
    }
    public void StartSpin() {
    	speed = 500;
    }
    public void Stop() {
    	speed--;
    	if (speed < 10) {
    		done.GetComponent<BoxCollider2D>().enabled = true;
    	}
    	if (speed <= 0) {
    		done.GetComponent<BoxCollider2D>().enabled = false;
    		speed = 0;    
    	}
    }
}
