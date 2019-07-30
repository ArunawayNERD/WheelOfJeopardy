using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionStore : MonoBehaviour
{   
	public Text sector, sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8, sector9, sector10, sector11; 
    private Dictionary<string, Dictionary<int, Question>> questions;

    private const int CATEGORY_INDEX = 0;
    private const int QUESTION_INDEX = 1;
    private const int ANSWER_INDEX = 2;
    private const int SCORE_INDEX = 3;

    void Awake()
    {
    	Dictionary<int, string[]> skeletalQuestions = new Dictionary<int, string[]>();
    	questions = new Dictionary<string, Dictionary<int, Question>>();
    	//List<Question> qStore = new List<Question>();
    	//List<Category> catStore = new List<Category>();
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

        int catCount = 0;
        for(int i=0; i < data.Length; i++)
        {
            if(i > 0) //row 0 is header info
            {
                string[] guestionData = data[i].Split(',');

                string category = guestionData[CATEGORY_INDEX];

                //We havnt run into this key yet so add its nested dict and update the sectors
                if (!questions.ContainsKey(category))
                {
                    sectors[catCount].text = category;
                    catCount++;

                    questions.Add(category, new Dictionary<int, Question>());
                }

                int score = int.Parse(guestionData[SCORE_INDEX]);
                string questionText = guestionData[QUESTION_INDEX];
                string answer = guestionData[ANSWER_INDEX];

                questions[category].Add(score, new Question(questionText, answer, score, category));
            }

        }

        //for (int row = 1; row < data.Length - 1; row++) {
        //	if (row == 1) {		
        //		//category
        //		cats = data[row].Split(',');
        //		for (int j = 0; j < cats.Length; j++) {
        //			sectors[j].text = cats[j];
        //			//catStore.Add(new Category(cats[j]));
        //			//Debug.Log(cats[j]);
        //		} 

        //		//Debug.Log(data[row]);   			
        //	}
        //}
        sectors[6].text = "Lose turn";
        sectors[7].text = "Free turn";
        sectors[8].text = "Bankrupt";
        sectors[9].text = "Player's choice";
        sectors[10].text = "Opponent's choice";
        sectors[11].text = "Double your Score";

        //Debug.Log(catStore[0].getQuestion().Count);
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

    public Question getQuestion(string category, int pointValue){
        Debug.Log("Question Store: Getting question " + category + " for " + pointValue);
        return  this.questions[category][pointValue];
    }
}
