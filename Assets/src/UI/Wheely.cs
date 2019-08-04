using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheely : MonoBehaviour
{
    private GameEngine gameEngine;
    private Sector[] sectors;

    public Sector[] Sectors { get => sectors; set => sectors = value; }



    // Start is called before the first frame update
    void Start()
    {
        //Stand-in to get code to run
        sectors = new Sector[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetCategory(int categIndex)
    {
        return sectors[categIndex].Name;
    }


}
