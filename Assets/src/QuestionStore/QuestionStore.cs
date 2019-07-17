using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionStore : MonoBehaviour
{   
    private Dictionary<string, Dictionary<int, string[]>> questions;

    void Awake(){
        questions = new Dictionary<string, Dictionary<int, string[]>>();

        Dictionary<int, string[]> skeletalQuestions = new Dictionary<int, string[]>();

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
