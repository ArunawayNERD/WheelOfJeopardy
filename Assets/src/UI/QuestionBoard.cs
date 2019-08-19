using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionBoard : MonoBehaviour
{
	public GameEngine gameEngine;

    public TextMeshProUGUI[] headers;
    public Button[] catButtons;

    //probably not the best way to do this but speed over the best code atm
    public TextMeshProUGUI[] catOneTexts;
    public TextMeshProUGUI[] catTwoTexts;
    public TextMeshProUGUI[] catThreeTexts;
    public TextMeshProUGUI[] catFourTexts;
    public TextMeshProUGUI[] catFiveTexts;
    public TextMeshProUGUI[] catSixTexts;

    // Start is called before the first frame update
    void Start()
    {
        //Get the question data
        string[] categories = gameEngine.getQuestionCategories();

        for(int i = 0; i < categories.Length; i++)
        {
            headers[i].SetText(categories[i]);
        }

        //Uncommented for now for testing
        //  this.setBoardInteractable(false);

    }

    public void setBoardInteractable(bool[] interactable)
    {
        for(int i=0; i < interactable.Length; i++)
        {
            this.catButtons[i].interactable = interactable[i];
        }
    }

    public void SetBoardInteractable(bool interactable)
    {
        foreach(Button catButton in this.catButtons)
        {
            catButton.interactable = interactable;
        }
    }

    public void resetForRoundTwo()
    {
        //go through the text fields and turn them on if they were off and double the score text
        TextMeshProUGUI[][] allTexts = { catOneTexts, catTwoTexts, catThreeTexts, catFourTexts, catFiveTexts, catSixTexts };

        for (int i = 0; i < allTexts.Length; i++)
        {
            TextMeshProUGUI[] catTexts = allTexts[i];


            catButtons[i].interactable = true;

            for (int j = 0; j < catTexts.Length; j++)
            {
                TextMeshProUGUI catText = catTexts[j];

                catText.SetText(((j + 1) * 400).ToString());
            }
        }
    }

    // We only need this if the player is allowed to choose their own category.
    // NOTE: In Unity, Wheel indices for categories must match QuestionBoard indices for categories.
    public void HandleCategoryClicked(int categIndex)
    {
        gameEngine.CategorySelected(categIndex);
    }

    public void ReceiveQuestionAnswered(Dictionary<string, int> questionsAnswered)
    {
        for(int i =0; i < headers.Length; i++)
        {
            string cat = this.headers[i].text;

            int numAnswered = questionsAnswered[cat];
            TextMeshProUGUI[] texts = null;
            switch (i)
            {
                case 0:
                    texts = this.catOneTexts;
                    break;
                case 1:
                    texts = this.catTwoTexts;
                    break;
                case 2:
                    texts = this.catThreeTexts;
                    break;
                case 3:
                    texts = this.catFourTexts;
                    break;
                case 4:
                    texts = this.catFiveTexts;
                    break;
                case 5:
                    texts = this.catSixTexts;
                    break;
            }

            if (numAnswered < 6)
            {
                for (int j = 1; j <= numAnswered; j++)
                {
                    texts[j - 1].SetText("");
                }

                if (numAnswered >= 5)
                {
                    this.catButtons[i].interactable = false;
                }
            }
        }
    }
}
