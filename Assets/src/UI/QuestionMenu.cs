using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionMenu : MonoBehaviour
{
    public GameObject menuGraphics;

    public Text title;
	public Text displayText;

	public Button showAnswer;
    public Button correct;
    public Button incorrect;


    // Start is called before the first frame update
    void Awake()
    {
        this.ResetMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ToggleVisible(bool show) {
		this.menuGraphics.SetActive(show);
	}

	private void ResetMenu() {
        this.displayText.text = "Question";

        this.showAnswer.gameObject.SetActive(true);
        this.correct.gameObject.SetActive(false);
        this.incorrect.gameObject.SetActive(false);


    }

    private void SwitchToAnswerMode()
    {
        this.displayText.text = "Question";

        this.showAnswer.gameObject.SetActive(true);
        this.correct.gameObject.SetActive(false);
        this.incorrect.gameObject.SetActive(false);
    }


}
