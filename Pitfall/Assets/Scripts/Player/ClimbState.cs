using UnityEngine;
using System.Collections;

public class ClimbState : IPlayerState {

    private readonly PlayerController player;

    // previous gravity state
    private float prevGravity;

    public ClimbState(PlayerController playerController)
    {
        player = playerController;
    }


    // physics related update
    public void fixedUpdate()
    {
        player.rigidbody2d.velocity = new Vector2(0.0f, player.verticalAxis * player.speed);
    }

    // called every frame
    public void update()
    {
        if (player.jumpPressed)
        {
            player.horizontalForce = player.horizontalAxis * player.speed;
            player.ChangeState("jump");
        }
    }

    // collision with a trigger
    public void onTriggerEnter(Collider2D coll)
    {

    }

    public void onTriggerStay(Collider2D coll)
    {

    }

    public void onTriggerExit(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ladder"))
        {
            player.collidedWith = null;
            player.ChangeState("ground");
        }
    }


    // called when state entered
    public void enter()
    {
        // save the player's gravity state and disable gravity
        prevGravity = player.rigidbody2d.gravityScale;
        player.rigidbody2d.gravityScale = 0.0f;
        player.animator.SetBool("climbing", true);
    }

    // called before state left
    public void exit()
    {
        // restore the player's previous gravity state
        player.rigidbody2d.gravityScale = prevGravity;
        player.animator.SetBool("climbing", false);
    }
}
