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

    public void HandleCategoryClicked(int category)
    {
        gameEngine.categorySelected(category);
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

            for(int j = 1; j <= numAnswered; j++)
            {
                texts[j - 1].SetText("");
            }

            if(numAnswered >= 5)
            {
                this.catButtons[i].interactable = false;
            }
        }
    }
}
