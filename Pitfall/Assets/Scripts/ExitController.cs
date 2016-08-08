using UnityEngine;
using System.Collections;

/**
 * Toggles the parent segment on or off
 */
public class ExitController : MonoBehaviour {

    private SegmentData segment;

    // used to keep track of player collision and prevent multiple triggers
    private bool colliding = false;

    void Awake ()
    {
        // get the parent segment that this exit belongs to
        segment = (SegmentData)transform.parent.GetComponent(typeof(SegmentData));
    }

    /**
     * Set colliding true once player has entered
     */
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

    /**
     * If the player leaves the exit, toggle this segment
     */
	void OnTriggerExit2D (Collider2D coll)
    {
        // only toggle if player and colliding is still true
        // prevents this from being triggered multiple times by
        // the players colliders
        if (coll.gameObject.tag == "Player" && colliding)
        {
            segment.SendMessage("Toggle");
            colliding = false;
        }
    }
}
