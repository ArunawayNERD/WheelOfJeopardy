using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterQAsBtn : MonoBehaviour
{
    public GameObject enterQAsMenuGraphics;
    public GameObject enterQAsMenu;

    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void HandleButtonClicked()
    {
        this.enterQAsMenuGraphics.SetActive(true);
        this.enterQAsMenu.SetActive(true);
        print("QA Menu active");
    }

}
