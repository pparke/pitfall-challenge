using UnityEngine;
using System.Collections;

/**
 * Handles higher level interaction between the game or level and their contained objects.
 * Updates UI state along with player score and lives.  Resets player to saved checkpoints
 * on death.
 */
public class GameManager : MonoBehaviour {

    private int maxTime = 60 * 20;
    private int timeRemaining;
    private int score = 2000;
    private PlayerController player;
    public GameObject checkpoint;
    public GameObject playerStart;

	// Use this for initialization
	void Start () {
        timeRemaining = maxTime;

        // start the clock
        InvokeRepeating("Tick", 0, 1.0f);

        // get the player
        player = (PlayerController)GameObject.Find("Player").GetComponent(typeof(PlayerController));
    }
	
	// called once per second to update the clock
	void Tick () {
        timeRemaining -= 1;
        UIManager.SetTime((int)Mathf.Floor(timeRemaining / 60), (int)timeRemaining % 60);
	}

    /**
     * Reset the player to the last checkpoint.
     */
    public void ResetPlayer ()
    {
        Debug.Log("Player reset");
        player.transform.position = checkpoint.transform.position;
    }

    /**
     * Reset the game to the start condition.
     */
    public void Reset ()
    {
        Debug.Log("Game reset");
        
        player.transform.position = playerStart.transform.position;
    }

    /**
     * Set the current checkpoint to the provided gameobject
     */
    public void SetCheckpoint (GameObject check)
    {
        checkpoint = check;
    }
}
