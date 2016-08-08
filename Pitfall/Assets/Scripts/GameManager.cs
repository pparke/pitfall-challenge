using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


/**
 * Handles higher level interaction between the game or level and their contained objects.
 * Updates UI state along with player score and lives.  Resets player to saved checkpoints
 * on death.
 */
public class GameManager : MonoBehaviour {

    public int gameDuration = 20;
    private int timeRemaining;
    private PlayerController player;
    public GameObject checkpoint;
    public GameObject playerStart;

    // delay for button press
    public float buttonDelay = 0.2f;
    private float lastPress = 0.0f;

    private int durationInSeconds
    {
        get
        {
            return gameDuration * 60;
        }
    }

    void Awake ()
    {
        // get the player
        player = (PlayerController)GameObject.Find("Player").GetComponent(typeof(PlayerController));
    }

	// Use this for initialization
	void Start () {
        timeRemaining = durationInSeconds;

        // start the clock
        InvokeRepeating("Tick", 0, 1.0f);
    }

    void Update ()
    {
        if (Input.GetAxis("Cancel") != 0.0f && lastPress + buttonDelay < Time.time)
        {
            lastPress = Time.time;
            ToggleExitPanel();
        }
    }
	
    /**
	 * Called once per second to update the clock
     */
	void Tick () {
        timeRemaining -= 1;
        UIManager.SetTime((int)Mathf.Floor(timeRemaining / 60), (int)timeRemaining % 60);
	}

    /**
     * Reset the player to the last checkpoint.
     */
    public void ResetPlayer ()
    {
        player.Reposition(new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y + 5));
        player.Invoke("Revive", 0.5f);
    }

    /**
     * Reset the game to the start condition.
     */
    public void Reset ()
    {
        Debug.Log("Game reset");

        timeRemaining = durationInSeconds;
        player.Reposition(playerStart.transform.position);
        player.Reset();
        player.Invoke("Revive", 0.5f);
    }

    /**
     * Set the current checkpoint to the provided gameobject
     */
    public void SetCheckpoint (GameObject check)
    {
        checkpoint = check;
    }

    public void UpdateUI ()
    {
        // update score display
        UIManager.SetScore(player.score);
        // update the lives display
        UIManager.SetLives(player.lives);
    }

    /**
     * Quit the game
     */
    public void ExitGame()
    {
        Application.Quit();
    }

    /**
     * Go to the main menu
     */
    public void MainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /**
     * Start the main gameplay scene
     */
    public void StartGame ()
    {
        SceneManager.LoadScene("Play");
    }

    /**
     * Show and hide the exit panel
     */
    public void ToggleExitPanel ()
    {
        UIManager.ToggleExitPanel();
    }
}
