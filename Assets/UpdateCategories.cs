using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCategories : MonoBehaviour
{
    public string content;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas/SkeletalMenu/input").SetActive(false);
        GameObject.Find("Canvas/SkeletalMenu/Go").SetActive(false);
        Text cat1 = GameObject.Find("Canvas/SkeletalMenu/Cat1").GetComponent<Text>();   
        Text cat2 = GameObject.Find("Canvas/SkeletalMenu/Cat2").GetComponent<Text>();
        Text cat3 = GameObject.Find("Canvas/SkeletalMenu/Cat3").GetComponent<Text>();

        TextAsset categories = (TextAsset)Resources.Load("cats");
        content = categories.text;
        string[] a = content.Split(',');

        cat1.text = a[0];
        cat2.text = a[1];
        cat3.text = a[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
