using UnityEngine;
using System.Collections;

/**
 * State providing behaviour for player when damaged.
 * based on https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public class DamageState : IPlayerState {

    private readonly PlayerController player;

    private float enterTime;
    private float exitDelay = 0.25f;

    public DamageState (PlayerController playerController)
    {
        player = playerController;
    }


    /**
     * Exit the damage state after a suitable delay and once the player is grounded again.
     */
    public void fixedUpdate ()
    {
        if (Time.time > enterTime + exitDelay && player.CheckGrounded())
        {
            player.ChangeState("ground");
        }
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

    public void onTriggerExit(Collider2D coll)
    {

    }

    /**
     * Play the damage animation, record the start time,
     * and stop player movement.
     */
    public void enter ()
    {
        player.animator.SetTrigger("damage");
        enterTime = Time.time;
        player.rigidbody2d.velocity = Vector2.zero;
    }

    // called before state left
    public void exit ()
    {
    }
}
