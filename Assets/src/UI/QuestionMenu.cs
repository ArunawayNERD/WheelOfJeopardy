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
    public TextMeshProUGUI timeDisplayed;
    public TextMeshProUGUI timeDisplayed2;

    public Button showAnswer;
    public Button correct;
    public Button incorrect;
    public Button showQuestion;

    private Question selectedQuestion;

    private float timer;
    private bool timerActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        this.ResetMenu();
    }

    private void Update()
    {
        if (timerActive && timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            this.timeDisplayed.SetText(timer.ToString("F"));
            this.timeDisplayed2.SetText(timer.ToString("F"));
        }
        else if (timerActive && timer <= 0.0f)
        {
            this.SwitchToAnswerMode(true);
        }
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
        this.SwitchToAnswerMode(false);
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

    public void StartTimer()
    {
        this.timeDisplayed.SetText("5.00");
        this.timeDisplayed2.SetText("5.00");
        timer = 5;
        timerActive = true;
    }

    public void HandleTimerRunout()
    {
        gameEngine.handleTimerRunOut();
        UpdateVisability(false);
    }

    public void ResetTimer(bool timesUp)
    {
        this.timeDisplayed.SetText("00.00");
        this.timeDisplayed2.SetText("00.00");
        timer = 0.0f;
        timerActive = false;
        if (timesUp)
        {
            this.HandleTimerRunout();
        }
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

        this.StartTimer();

        this.showQuestion.gameObject.SetActive(false);
        this.showAnswer.gameObject.SetActive(true);
        this.correct.gameObject.SetActive(false);
        this.incorrect.gameObject.SetActive(false);
    }

    private void SwitchToAnswerMode(bool timesUp)
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
        this.ResetTimer(timesUp);

        if (timesUp) // show answer briefly-not working
        {
            //System.DateTime ts = System.DateTime.Now + System.TimeSpan.FromSeconds(3);
            //do { } while (System.DateTime.Now < ts);
        }
        else
        {
            this.correct.gameObject.SetActive(true);
            this.incorrect.gameObject.SetActive(true);
        }
    }
}
