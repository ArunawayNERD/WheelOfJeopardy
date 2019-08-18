using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelArrow : MonoBehaviour
{
    public GameEngine gameEngine;
    public Wheel wheel;

    public GameObject arrow;
    public float speed = 0;
    public bool spinning = false;


    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    //how to tell what the wheel stopped on
    void OnCollisionEnter2D(Collision2D col)
    {


        Debug.Log(col.gameObject.GetComponent<Text>().text);
        for (int i = 0; i < gameEngine.sectorList.Count; i++)
        {
            if (gameEngine.sectorList[i].name == col.gameObject.GetComponent<Text>().text)
            {
                Debug.Log(gameEngine.sectorList[i].name + gameEngine.sectorList[i].type);
                gameEngine.SectorLandedOn(gameEngine.sectorList[i]);
                break;
            }
        }
    }

    void Rotate()
    {
        wheel.transform.Rotate(0, 0, -speed * Time.deltaTime);
        if (speed > 0)
        {
            Stop();
        }
    }
    public void StartSpin()
    {
        speed = 500;
    }
    public void Stop()
    {
        speed--;
        if (speed < 10)
        {
            arrow.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (speed <= 0)
        {
            arrow.GetComponent<BoxCollider2D>().enabled = false;
            speed = 0;
            //this.spinsLeftInRound = this.spinsLeftInRound - 1;
            //this.spinsLeft.SetText(this.spinsLeftInRound.ToString());
        }
    }
}
