using UnityEngine;
using System.Collections;

public class ExitController : MonoBehaviour {

    private SegmentData segment;

    void Awake ()
    {
        // get the parent segment that this exit belongs to
        segment = (SegmentData)transform.parent.GetComponent(typeof(SegmentData));
    }

	void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            segment.Exit();
        }
    }
}
