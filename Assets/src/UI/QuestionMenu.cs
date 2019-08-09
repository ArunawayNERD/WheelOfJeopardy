using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionMenu : MonoBehaviour
{
    public GameEngine gameEngine;
    public GameObject menuGraphics;

    public TextMeshProUGUI title;
	public TextMeshProUGUI displayText;

	public Button showAnswer;
    public Button correct;
    public Button incorrect;
    public Button showQuestion;
    //public GameObject myButton;

    private Question selectedQuestion;


    // Start is called before the first frame update
    void Awake()
    {
        this.ResetMenu();
    }

	public void UpdateVisability(bool show)
	{
		this.menuGraphics.SetActive(show);
    }

    public void ReceiveQuestion(Question selected)
    {
        this.selectedQuestion = selected;
        this.ResetMenu();
        UpdateVisability(true);
    }

    public void HandleShowAnswerClicked()
    {
        this.SwitchToAnswerMode();
    }

    public void HandleShowQuestionClicked()
    {
        this.SwitchToQuestionMode();
    }

    public void HandleAnswerClicked(bool correct)
    {
        gameEngine.questionAnswered(this.selectedQuestion.points, correct);
        UpdateVisability(false);
    }
    
	private void ResetMenu()
    {
        this.title.SetText("Category");

        if (this.selectedQuestion != null)
        {
            this.displayText.SetText(selectedQuestion.category);
        }
        else
        {
            this.displayText.SetText("");
        }
        
        this.showQuestion.gameObject.SetActive(true);
        this.showAnswer.gameObject.SetActive(false);
        this.correct.gameObject.SetActive(false);
        this.incorrect.gameObject.SetActive(false);
    }

    private void SwitchToQuestionMode()
    {
        this.title.SetText("Question");

        if (this.selectedQuestion != null)
        {
            this.displayText.SetText(selectedQuestion.question);
        }
        else
        {
            this.displayText.SetText("");
        }
        
        this.showQuestion.gameObject.SetActive(false);
        this.showAnswer.gameObject.SetActive(true);
        this.correct.gameObject.SetActive(false);
        this.incorrect.gameObject.SetActive(false);
    }

    private void SwitchToAnswerMode()
    {
        this.title.SetText("Answer");

        if (this.selectedQuestion != null)
        {
            this.displayText.SetText(selectedQuestion.answer);
        }
        else
        {
            this.displayText.SetText("");
        }

        this.showAnswer.gameObject.SetActive(false);
        this.correct.gameObject.SetActive(true);
        this.incorrect.gameObject.SetActive(true);
    }
}
