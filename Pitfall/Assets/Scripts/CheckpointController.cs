using UnityEngine;
using System.Collections;

/**
 * Save the player's position when they reach this point.
 */
public class CheckpointController : MonoBehaviour {

    private GameManager gameManager;

    /**
     * Get a reference to the game manager and the parent segment.
     */
    void Awake()
    {
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
    }

    /**
     * Call enter on segment and save checkpoint
     */
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gameManager.SetCheckpoint(gameObject);
        }
    }
}
