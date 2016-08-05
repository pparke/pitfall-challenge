using UnityEngine;
using System.Collections;

public class DeadState : IPlayerState {

    private readonly PlayerController player;

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

    // called when state entered
    public void enter ()
    {
        player.animator.SetBool("dead", true);
        player.rigidbody2d.velocity = Vector2.zero;
    }

    // called before state left
    public void exit ()
    {
        player.animator.SetBool("dead", false);

    }
}
