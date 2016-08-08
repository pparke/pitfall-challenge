using UnityEngine;
using System.Collections;

/**
 * Basic pickup that increases player's score
 */
public class PickupController : MonoBehaviour {

    public GameObject particle;

    /**
     * Increase the player's score, instantiate a particle system,
     * and then self destruct
     */
	void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.SendMessage("AddPoints", 100);
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
