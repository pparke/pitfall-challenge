using UnityEngine;
using System.Collections;

/**
 * Controls the mouth opening and closing behaviour of the crocodile obstacle.
 * The player can stand on the crocodile's head at any time but can only stand
 * on its mouth when it is in the closed state.
 */
public class CrocodileMouthController : MonoBehaviour {

    private Animator animator;
    // reference to the state machine behaviour used to listen for events during animation
    private CrocodileBehaviour[] behaviours;

    public bool open = false;
    public float openDuration = 3.0f;
    public float closedDuration = 3.0f;

    /**
     * Setup references to the animator and the behaviours on the animator.
     * Give each behaviour a reference to this script so that it can trigger
     * methods based on the animation state.
     */
	void Awake () {
        animator = (Animator)transform.parent.GetComponent(typeof(Animator));

        // get the associated crocodile behaviour
        behaviours = animator.GetBehaviours<CrocodileBehaviour>();
        // set a reference to this script on each behaviour
        for (int i = 0; i <  behaviours.Length; i++)
        {
            behaviours[i].mouth = this;
        }
	}

    /**
     * Set the mouth to the open state
     */
    public void OpenMouth ()
    {
        open = true;
        animator.SetBool("open", true);
    }

    /**
     * Set the mouth to the closed state
     */
    public void CloseMouth ()
    {
        open = false;
        animator.SetBool("open", false);
    }

    /**
     * Kill the player if it comes in contact with the
     * crocodile's mouth while it is open
     */
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (open)
        {
            if (coll.gameObject.tag == "Player")
            {
                Debug.Log("sending kill message");
                coll.gameObject.SendMessage("Kill");
            }
        }
        
    }
}
