using UnityEngine;
using System.Collections;

/**
 * Will pan the camera that it is attached to as the player moves,
 * uses a buffer zone to limit camera movement.
 * based on code found in Mastering Unity 2D Game Development by Simon Jackson https://www.packtpub.com/game-development/mastering-unity-2d-game-development
 */
public class FollowCamera : MonoBehaviour {

    // buffer zone size
    public float buffer = 0.5f;
    // camera pan speed
    public float panSpeed = 2.5f;

    // limit camera movement between these two y values
    public float minY = -0.1f;
    public float maxY = 2.0f;

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
            target.y = Mathf.Clamp(Mathf.Lerp(transform.position.y, player.position.y, panSpeed * Time.fixedDeltaTime), minY, maxY);
        }

        // move the camera by the required amount
        transform.position = target;
	}

    /**
     * Returns true if the player is outside the buffer zone
     */
    bool pastBuffer ()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > buffer || Mathf.Abs(transform.position.y - player.position.x) > buffer;
    }
}
