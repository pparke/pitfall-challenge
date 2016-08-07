using UnityEngine;
using System.Collections;

/**
 * Toggles the parent segment on or off
 */
public class ExitController : MonoBehaviour {

    private SegmentData segment;

    void Awake ()
    {
        // get the parent segment that this exit belongs to
        segment = (SegmentData)transform.parent.GetComponent(typeof(SegmentData));
    }

    /**
     * If the player crosses the exit, toggle this segment
     */
	void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            segment.Toggle();
        }
    }
}
