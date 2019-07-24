using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public bool spinning = true;
	public float speed = -500;
	//public GameObject circle;

	void Update() {
		Spin();
	}

	public void Spin() {
    	transform.Rotate(0, 0,speed*Time.deltaTime);
    	if (speed < 0) {
    		speed++;
    	}
    	if (speed == 0) {
    		spinning = false;
    		speed = 0;
    	}
    }
}
