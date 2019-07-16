using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    Text myText = GameObject.Find("Canvas/SkeletalMenu/Categories").GetComponent<Text>();
    int s = 0;
    // Start is called before the first frame update
    void Start()
    {
        Text cat1 = GameObject.Find("Canvas/SkeletalMenu/Categories").GetComponent<Text>();
        Text cat2 = GameObject.Find("Canvas/SkeletalMenu/Categories").GetComponent<Text>();
        Text cat3 = GameObject.Find("Canvas/SkeletalMenu/Categories").GetComponent<Text>();
        cat1.text = "now?";
        cat2.text = "now??";
        cat3.text = "now???";
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
