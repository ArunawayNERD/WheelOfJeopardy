using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Hide_QA : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Canvas/question_answer").SetActive(false);
        //panel.gameObject.SetActive(false);
    }
    public void showQuestion()
    {
        var q = GameObject.Find("Canvas/SkeletalMenu/Question").GetComponent<Text>();
        TextAsset categories = (TextAsset)Resources.Load("questions");
        var content = categories.text;
        q.text = "Q: " + content.ToString();
        GameObject.Find("Canvas/SkeletalMenu/input").SetActive(true);
        GameObject.Find("Canvas/SkeletalMenu/Go").SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
