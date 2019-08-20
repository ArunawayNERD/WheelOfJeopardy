using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class QuestionStore : MonoBehaviour
{   
	public Text sector, sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8, sector9, sector10, sector11;

    private Dictionary<string, Dictionary<int, Question>> questions;
    private Dictionary<string,int> questionsAnswered;

    private const int CATEGORY_INDEX = 0;
    private const int QUESTION_INDEX = 1;
    private const int ANSWER_INDEX = 2;
    private const int SCORE_INDEX = 3;

    private bool dataEntered;
    private int dataSrcChange = 0;

    public bool DataEntered { get => dataEntered; set => dataEntered = value; }

    void Awake()
    {
    	questions = new Dictionary<string, Dictionary<int, Question>>();
        questionsAnswered = new Dictionary<string, int>();

        TextAsset userInput;

        if (dataEntered)
        {
            userInput = Resources.Load<TextAsset>("EnteredQuestionData1");
        }
        else
        {
            userInput = Resources.Load<TextAsset>("QuestionData");
        }
    	string[] data = userInput.text.Split('\n');
        //Debug.Log(userInput.text);

        int catCount = 0;
        for(int i=0; i < data.Length; i++)
        {
            if(i > 0) //row 0 is header info
            {
                string[] questionData = data[i].Split(',');
                string category = questionData[CATEGORY_INDEX];
                if (!category.Equals("")) // just in case extra space at end of csv input file
                {

                    //We havent run into this key yet so add its nested dict and update the sectors
                    if (!questions.ContainsKey(category))
                    {
                        catCount++;
                        
                        questions.Add(category, new Dictionary<int, Question>());
                        questionsAnswered.Add(category, 0);
                    }
                    
                    int score = int.Parse(questionData[SCORE_INDEX]);
                    string questionText = questionData[QUESTION_INDEX];
                    string answer = questionData[ANSWER_INDEX];


                    questions[category].Add(score, new Question(questionText, answer, score, category));
                }
            }

        }
     
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (dataEntered && dataSrcChange == 0)
        {
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
            Debug.Log("QuestionStore reactivated with entered data");
        }
        //{
        //    dataSrcChange++;

        //    questions = new Dictionary<string, Dictionary<int, Question>>();
        //    questionsAnswered = new Dictionary<string, int>();

        //    TextAsset userInput = Resources.Load<TextAsset>("EnteredQuestionData1");

        //    string[] data = userInput.text.Split('\n');
        //    //Debug.Log(userInput.text);

        //    int catCount = 0;
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        if (i > 0) //row 0 is header info
        //        {
        //            string[] questionData = data[i].Split(',');
        //            string category = questionData[CATEGORY_INDEX];
        //            if (!category.Equals("")) // just in case extra space at end of csv input file
        //            {

        //                //We havent run into this key yet so add its nested dict and update the sectors
        //                if (!questions.ContainsKey(category))
        //                {
        //                    catCount++;

        //                    questions.Add(category, new Dictionary<int, Question>());
        //                    questionsAnswered.Add(category, 0);
        //                }

        //                int score = int.Parse(questionData[SCORE_INDEX]);
        //                string questionText = questionData[QUESTION_INDEX];
        //                string answer = questionData[ANSWER_INDEX];


        //                questions[category].Add(score, new Question(questionText, answer, score, category));
        //            }
        //        }

        //    }
        //}
    }

    public string[] getCategories(){
        return this.questions.Keys.ToArray();
    }

    public Question getQuestion(string category, int pointValue) {
        if (pointValue > 1000)
        { 
            return null;
        }
        Debug.Log("Question Store: Getting question " + category + " for " + pointValue);

        this.questionsAnswered[category] = this.questionsAnswered[category] + 1;
        return  this.questions[category][pointValue];
    }

    public Dictionary<string, int> getQuestionsAnswered()
    {
        return this.questionsAnswered;
    }
}
