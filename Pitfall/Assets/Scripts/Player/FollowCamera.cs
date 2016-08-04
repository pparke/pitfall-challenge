using UnityEngine;
using System.Collections;

/**
 * Will pan the camera that it is attached to as the player moves,
 * uses a buffer zone to limit camera movement.
 */
public class FollowCamera : MonoBehaviour {

    // buffer zone size
    public float buffer = 0.5f;
    // camera pan speed
    public float panSpeed = 2.5f;

    // the player reference to be assigned in the inspector
    public Transform player;
	
	void FixedUpdate ()
    {
        // default to current camera position
        Vector3 target = transform.position;

        // if the player has moved out of the buffer zone, set the target camera position so
        // it smoothly pans to the new location
        if (pastBuffer())
        {
            target.x = Mathf.Lerp(transform.position.x, player.position.x, panSpeed * Time.fixedDeltaTime);
        }

        // move the camera by the required amount
        transform.position = target;
	}

    /**
     * Returns true if the player is outside the buffer zone
     */
    bool pastBuffer ()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > buffer;
    }
}
