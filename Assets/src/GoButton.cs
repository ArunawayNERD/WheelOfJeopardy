using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{
    public void onSubmit()
    {
        var q = GameObject.Find("Canvas/SkeletalMenu/Question").GetComponent<Text>();
        var ans = GameObject.Find("Canvas/SkeletalMenu/input").GetComponent<InputField>();
        TextAsset answers = (TextAsset)Resources.Load("answers");
        var content = answers.text;
        if (ans.text.ToString().Trim() == content.ToString())
        {
            q.text = "Correct!";
        }
        else
        {
            q.text = "Incorrect!";
        }
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
