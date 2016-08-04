using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int maxTime = 60 * 20;
    private int timeRemaining;

	// Use this for initialization
	void Start () {
        timeRemaining = maxTime;

        // start the clock
        InvokeRepeating("Tick", 0, 1.0f);
	}
	
	// called once per second to update the clock
	void Tick () {
        timeRemaining -= 1;
        UIManager.SetTime((int)Mathf.Floor(timeRemaining / 60), (int)timeRemaining % 60);
	}
}
