using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionStore : MonoBehaviour
{   
	public Text sector, sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8, sector9, sector10, sector11; 
    private Dictionary<string, Dictionary<int, string[]>> questions;

    void Awake(){
    	Dictionary<int, string[]> skeletalQuestions = new Dictionary<int, string[]>();
    	questions = new Dictionary<string, Dictionary<int, string[]>>();
    	List<Question> qStore = new List<Question>();
    	List<Category> catStore = new List<Category>();
    	List<Text> sectors = new List<Text>();
    	sectors.Add(sector);
    	sectors.Add(sector1);
    	sectors.Add(sector2);
    	sectors.Add(sector3);
    	sectors.Add(sector4);
    	sectors.Add(sector5);
    	sectors.Add(sector6);
    	sectors.Add(sector7);
    	sectors.Add(sector8);
    	sectors.Add(sector9);
    	sectors.Add(sector10);
    	sectors.Add(sector11);
		string[] cats;
		
    	TextAsset userInput = Resources.Load<TextAsset>("QuestionData");
    	string[] data = userInput.text.Split('\n');
    	//Debug.Log(userInput.text);
    	
    	for (int row = 1; row < data.Length - 1; row++) {
    		if (row == 1) {		
    			//category
    			cats = data[row].Split(',');
    			for (int j = 0; j < cats.Length; j++) {
    				sectors[j].text = cats[j];
    				//catStore.Add(new Category(cats[j]));
    				//Debug.Log(cats[j]);
    			} 
    			
    			//Debug.Log(data[row]);   			
    		}
    	}
    	sectors[6].text = "Lose turn";
    	sectors[7].text = "Free turn";
    	sectors[8].text = "Bankrupt";
    	sectors[9].text = "Player's choice";
    	sectors[10].text = "Opponent's choice";
    	sectors[11].text = "Double your Score";
    	
    	//Debug.Log(catStore[0].getQuestion().Count);
        
        
        string[] question1 = {"2+2", "4"};
        string[] question2 = {"2+2", "4"};
        string[] question3 = {"2+2", "4"};
        string[] question4 = {"2+2", "4"};
        string[] question5 = {"2+2", "4"};

        skeletalQuestions.Add(200, question1);
        skeletalQuestions.Add(400, question2);
        skeletalQuestions.Add(600, question3);
        skeletalQuestions.Add(800, question4);
        skeletalQuestions.Add(1000, question5);

        questions.Add("Math", skeletalQuestions);
		



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] getCategories(){
        return this.questions.Keys.ToArray();
    }

    public string[] getQuestion(string category, int pointValue){
        Debug.Log("Question Store: Getting question " + category + " for " + pointValue);
        return  this.questions[category][pointValue];
    }
}
