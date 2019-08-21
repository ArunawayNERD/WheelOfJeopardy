using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Wheel : MonoBehaviour
{
    private Sector[] sectorObjs;

    public Text sector, sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8, sector9, sector10, sector11;

    private Dictionary<string, Dictionary<int, Question>> questions;

    private const int CATEGORY_INDEX = 0;

    public Sector[] SectorObjs { get => sectorObjs; set => sectorObjs = value; }

    //public GameObject menuGraphics;
    public Button spinWheel;

    private bool dataEntered;
    private int dataSrcChange = 0;

    public bool DataEntered { get => dataEntered; set => dataEntered = value; }

    void Awake()
    {
        this.loadNewSectors("QuestionData");
    }

    public void switchToRoundTwo()
    {
        Debug.Log("Switching wheel to match round 2 categories.");
        this.loadNewSectors("QuestionData2");
    }

    public void switchToEnteredData1()
    {
        Debug.Log("Switching wheel to match newly entered categories.");
        this.loadNewSectors("EnteredQuestionData1");
    }
    public void switchToEnteredData2()
    {
        Debug.Log("Switching wheel to match newly entered categories.");
        this.loadNewSectors("EnteredQuestionData2");
    }


    void loadNewSectors(string fileName)
    {
        questions = new Dictionary<string, Dictionary<int, Question>>();
        sectorObjs = new Sector[12];
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
        TextAsset userInput = Resources.Load<TextAsset>(fileName);

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
                    sectorObjs[catCount] = new Sector(category, "Category");
                    catCount++;

                    questions.Add(category, new Dictionary<int, Question>());
                }
            }
        }

        sectors[6].text = "Lose turn";
        sectorObjs[6] = new Sector("Lose turn", "Non-category");
        sectors[7].text = "Free turn";
        sectorObjs[7] = new Sector("Free turn", "Non-category");
        sectors[8].text = "Bankrupt";
        sectorObjs[8] = new Sector("Bankrupt", "Non-category");
        sectors[9].text = "Player's choice";
        sectorObjs[9] = new Sector("Player's choice", "Non-category");
        sectors[10].text = "Opponent's choice";
        sectorObjs[10] = new Sector("Opponent's choice", "Non-category");
        sectors[11].text = "Double your Score";
        sectorObjs[11] = new Sector("Double your Score", "Non-category");
        
    }

    internal string GetCategory(int categIndex)
    {
        return sectorObjs[categIndex].Name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
}
