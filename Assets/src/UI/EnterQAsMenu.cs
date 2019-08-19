using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterQAsMenu : MonoBehaviour
{
    public GameEngine gameEngine;

    public GameObject menuGraphics;
    public GameObject catInput;
    public GameObject qInput;
    public GameObject aInput;

    public GameObject title;
    public GameObject enterCat;
    public GameObject enterQ;
    public GameObject enterA;

    public Button next;

    private int catIndex = 1;
    private int NUM_CATS = 6;
    private string input;
    

    // Start is called before the first frame update
    void Awake()
    {
        this.UpdateVisibility(false);
    }

	public void UpdateVisibility(bool show)
	{
		this.menuGraphics.SetActive(show);
    }

    public void HandleNextClicked(bool useToken)
    {
        if (catIndex <= NUM_CATS)
        {
            input = catInput.GetComponent<Text>().text;
            print(input);         
            catIndex++;
            if (catIndex > NUM_CATS)
            {
                // Deactivate category input prompts.
                catInput.SetActive(false);
                enterCat.SetActive(false);
                // Activate Q/A input prompts;
                qInput.SetActive(true);
                aInput.SetActive(false);
                enterQ.SetActive(true);
                enterA.SetActive(true);      
            }
            else
            {
                catInput.SetActive(false);
                catInput.SetActive(true);
            }
        }
        else
        {
            print("Done entering categories");
        }
    }
}
