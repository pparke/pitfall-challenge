using UnityEngine;
using System.Collections;

/**
 * Allows a pit or water hazard to open and close,
 * kills the player on contact.
 */
public class PitController : MonoBehaviour {

    private Animator animator;

    // reference to the state machine behaviour used to listen for events during animation
    private PitBehaviour[] behaviours;

    // does this pit act as quicksand (opens and closes)
    public bool quicksand = false;

    // is the pit currently open
    public bool open = false;

    // how long the pit remains open
    public float openDuration = 3.0f;

    // how long the pit remains closed
    public float closedDuration = 3.0f;

    /**
     * Setup references to the animator and the behaviours on the animator.
     * Give each behaviour a reference to this script so that it can trigger
     * methods based on the animation state.
     */
    void Awake()
    {
        animator = (Animator)transform.GetComponent(typeof(Animator));

        animator.enabled = quicksand;

        // get the associated pit behaviours
        behaviours = animator.GetBehaviours<PitBehaviour>();
        // set a reference to this script on each behaviour
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].pit = this;
        }
    }

    /**
     * Set the pit to the open state
     */
    public void OpenPit()
    {
        open = true;
        animator.SetBool("open", true);
    }

    /**
     * Set the pit to the closed state
     */
    public void ClosePit()
    {
        open = false;
        animator.SetBool("open", false);
    }

    /**
     * Kill the player if they collide with the pit
     */
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("Kill");
        }
     }
}
