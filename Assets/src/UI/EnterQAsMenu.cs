using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class EnterQAsMenu : MonoBehaviour
{
    public GameEngine gameEngine;
    private QuestionDataWriter qDataWriter;

    public GameObject menuGraphics;
    public GameObject catInput;
    public GameObject qInput;
    public GameObject aInput;

    public TextMeshProUGUI title;
    public TextMeshProUGUI enterCat;
    public TextMeshProUGUI enterQ;
    public TextMeshProUGUI enterA;

    public Button next;

    // catIndex is used for evaluating whether enough categories have been entered.
    private int catIndex;
    // qaCatIndex is used for determining the category of the Q/A pair entered.
    private int qaCatIndex;
    private int NUM_CATS;

    //private int qIndex;
    private int NUM_QS;

    private int ptVal;
    private int ptInc;

    private int round;
    private bool roundChange;

    private string input;

    private string enterCatBaseTxt;
    private string enterQBaseTxt;
    private string titleBaseTxt;
    private bool csvWritten;



    // Start is called before the first frame update
    void Awake()
    {
        this.UpdateVisibility(false);
    }

    private void Start()
    {
        csvWritten = false;

        catIndex = 1;
        NUM_CATS = 6;

        //qIndex = 0;
        NUM_QS = 5;

        qaCatIndex = 0;

        enterCatBaseTxt = enterCat.text;
        enterCat.SetText(enterCatBaseTxt + " 1");

        enterQBaseTxt = enterQ.text;

        titleBaseTxt = title.text;
        title.SetText(titleBaseTxt + " - Round 1");

        qDataWriter = new QuestionDataWriter(NUM_CATS, NUM_QS, gameEngine);

        round = 1;
        roundChange = false;
        ptVal = 100;
        ptInc = 100;
    }

    public void UpdateVisibility(bool show)
	{
		this.menuGraphics.SetActive(show);
    }

    public void HandleNextClicked()
    {
        catIndex++;
        // If this conditional passes, accept category input and prompt for next category input. This is done first within a round.
        if (catIndex <= NUM_CATS)
        {
            input = catInput.GetComponent<Text>().text;
            // First time this is called, catIndex will be 2. Address array properly.
            qDataWriter.Categories[catIndex - 2] = input;
            Debug.Log("Category entered: " + input);

            enterCat.SetText(enterCatBaseTxt + " " + catIndex.ToString());

            //catInput.GetComponent<Text>().text = "Enter Category...";
        }
        else if (catIndex == NUM_CATS + 1)
        {
            input = catInput.GetComponent<Text>().text;
            // First time this is called, catIndex will be 2. Address array properly.
            qDataWriter.Categories[catIndex - 2] = input;
            Debug.Log("Category entered: " + input);

            // Deactivate category input prompts.
            catInput.SetActive(false);
            enterCat.gameObject.SetActive(false);

            // Activate Q/A input prompts.
            enterQ.gameObject.SetActive(true);
            enterA.gameObject.SetActive(true);
            print("Done entering categories");

            // Modify question input prompt for first Q/A pair.
            enterQ.SetText(enterQBaseTxt + " for cat. " + qDataWriter.Categories[qaCatIndex] + ", pt. val. " + ptVal.ToString());
        }
        // Otherwise, prompt for QA pair input. This is done second within a round.
        else
        {

            string q = qInput.GetComponent<Text>().text;
            string a = aInput.GetComponent<Text>().text;

            int ptIndex = ptVal / ptInc;
            int qDataIndex = 0;

            // NOTE: This is is highly specific to CSV formatting.
            // E.g. if ptVal = 200 and ptInc = 100, ptVal/ptInc = 2, suggesting we are entering questions in the second point increment. Each increment has NUM_CATS number of entries.
            // E.g. (cont.) This is important because the CSV goes in descending point-value order. Row is determined both by category and poitns increment.
            for (int i = 0; i < NUM_QS; i++)
            {
                if (ptVal / ptInc - 1 == i)
                {
                    qDataIndex = qaCatIndex + i * NUM_CATS;
                }
            }
            if (round == 1)
            {
                // Category goes first in a row.
                qDataWriter.QAData1[qDataIndex, 0] = qDataWriter.Categories[qaCatIndex];
                // Then comes question.
                qDataWriter.QAData1[qDataIndex, 1] = q;
                // Then comes answer.
                qDataWriter.QAData1[qDataIndex, 2] = a;
                // Then comes point value.
                qDataWriter.QAData1[qDataIndex, 3] = ptVal.ToString();
            }
            else
            {
                // Category goes first in a row.
                qDataWriter.QAData2[qDataIndex, 0] = qDataWriter.Categories[qaCatIndex];
                // Then comes question.
                qDataWriter.QAData2[qDataIndex, 1] = q;
                // Then comes answer.
                qDataWriter.QAData2[qDataIndex, 2] = a;
                // Then comes point value.
                qDataWriter.QAData2[qDataIndex, 3] = ptVal.ToString();
            }

            Debug.Log("Row entered");

            qaCatIndex++;

            // When qCatIndex equals the number of categories, all Q/A pairs have been entered for a constant point value -- one for each category.
            if (qaCatIndex == NUM_CATS)
            {
                // If point value suggests the end of round 1, switch to round 2.
                if (ptVal == 500 && round == 1)
                {
                    roundChange = true;
                    round = 2;
                    ptVal = 200;
                    ptInc = 200;
                    qaCatIndex = 0;
                }
                // If point value suggests the end of round 2, terminate processing.
                else if (ptVal == 1000 && round == 2)
                {
                    // Terminate processing by making window inactive and telling QuestionDataWriter to write to csv, then update data source.
                    this.UpdateVisibility(false);
                    qDataWriter.WriteToCSV();
                    csvWritten = true;
                }
                // Otherwise, increase the point value and reset the QA category counter.
                else
                {
                    ptVal += ptInc;
                    qaCatIndex = 0;
                }

            }

            if (!roundChange && !csvWritten)
            {
                // Update prompt so that user knows what to enter before they click next the next time.
                enterQ.SetText(enterQBaseTxt + " for cat. " + qDataWriter.Categories[qaCatIndex] + ", pt. val. " + ptVal.ToString());
            }
            

        }

        // Processing the last Q/A input pair suggested we were at a round change.
        // Reset catIndex to prompt collection of categories.
        if (roundChange)
        {
            roundChange = false;
            catIndex = 1;
            title.SetText(titleBaseTxt + " - Round " + round.ToString());
            // Reset categories list within QuestionDataWriter
            qDataWriter.Categories = new string[NUM_CATS];

            // Activate category input prompts.
            catInput.SetActive(true);
            enterCat.gameObject.SetActive(true);

            // Deactivate Q/A input prompts.
            enterQ.gameObject.SetActive(false);
            enterA.gameObject.SetActive(false);

            Debug.Log("Prompting for round 2 categories");

            // Update prompt to ask for round 2, category 1.
            enterCat.SetText(enterCatBaseTxt + " 1");
        }
    }
}
