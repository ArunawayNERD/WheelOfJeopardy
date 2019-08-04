using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public Text sector, sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8, sector9, sector10, sector11;

    private Dictionary<string, Dictionary<int, Question>> questions;

    private const int CATEGORY_INDEX = 0;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
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

        // Populate sectors using user input file.
        TextAsset userInput = Resources.Load<TextAsset>("QuestionData");
        string[] data = userInput.text.Split('\n');
        //Debug.Log(userInput.text);

        int catCount = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (i > 0) //row 0 is header info
            {
                string[] questionData = data[i].Split(',');

                string category = questionData[CATEGORY_INDEX];

                //We haven't run into this key yet so update the sectors and add its nested dictionary
                if (!questions.ContainsKey(category))
                {
                    sectors[catCount].text = category;
                    catCount++;

                    questions.Add(category, new Dictionary<int, Question>());
                }
            }
        }

        sectors[6].text = "Lose turn";
        sectors[7].text = "Free turn";
        sectors[8].text = "Bankrupt";
        sectors[9].text = "Player's choice";
        sectors[10].text = "Opponent's choice";
        sectors[11].text = "Double your Score";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
