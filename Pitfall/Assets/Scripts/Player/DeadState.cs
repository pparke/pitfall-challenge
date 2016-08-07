using UnityEngine;
using System.Collections;

/**
 * Defines player behaviour when dead
 * based on https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public class DeadState : IPlayerState {

    private readonly PlayerController player;

    // reference to the state machine behaviour used to listen for events during animation
    private PlayerDeadBehaviour playerDead;

    public DeadState (PlayerController playerController)
    {
        player = playerController;
    }


    // physics related update
    public void fixedUpdate ()
    {
    }

    // called every frame
    public void update ()
    {

    }

    // collision with a trigger
    public void onTriggerEnter (Collider2D coll)
    {
    }

    public void onTriggerStay (Collider2D coll)
    {
    }

    public void onTriggerExit (Collider2D coll)
    {

    }

    /**
     * Decrement lives and play the death animation
     */
public void enter ()
    {
        // stop all movement
        player.rigidbody2d.velocity = Vector2.zero;
        // remove lives
        player.lives -= 1;
        // start death animation
        player.animator.SetTrigger("dead");
        // get the PlayerDead behaviour
        playerDead = player.animator.GetBehaviour<PlayerDeadBehaviour>();
        // give it our player reference so it can change state once complete
        playerDead.player = player;
    }

    // called before state left
    public void exit ()
    {
        
    }
}
