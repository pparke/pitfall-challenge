using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

    private GameManager gameManager;
    private SegmentData segment;

    // Use this for initialization
    void Awake()
    {
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            segment.Enter();
            gameManager.SetCheckpoint(gameObject);
        }
    }
}
