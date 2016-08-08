using UnityEngine;
using System.Collections;

/**
 * Defines player behaviour when on the ground (walking)
 * based on https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public class GroundState : IPlayerState {

    private readonly PlayerController player;

    public GroundState(PlayerController playerController)
    {
        player = playerController;
    }

    /**
     * Check if the player is grounded and move based on horizontal input if they are
     */
    public void fixedUpdate()
    {
        if (player.CheckGrounded())
        {
            // calculate target speed based on horizontal input
            float targetSpeed = player.horizontalAxis * player.speed;

            // set the horizontal velocity based on how far the H axis is pressed
            player.rigidbody2d.velocity = new Vector2(targetSpeed, player.rigidbody2d.velocity.y);
        }

        // flip the sprite based on the direction it's moving
        player.FaceForward();
    }

    /**
     * Change to the jump state if jump is pressed
     */
    public void update()
    {
        if (player.jumpPressed)
        {
            player.ChangeState("jump");
        }
    }

    /**
     * Allow transition to the climb state
     */
    public void onTriggerEnter(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ladder") && player.verticalAxis != 0.0f)
        {
            player.collidedWith = coll;
            player.ChangeState("climb");
        }
    }

    public void onTriggerStay(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ladder") && player.verticalAxis != 0.0f)
        {
            player.collidedWith = coll;
            player.ChangeState("climb");
        }
    }

    public void onTriggerExit(Collider2D coll)
    {

    }


    // called when state entered
    public void enter()
    {
    }

    // called before state left
    public void exit()
    {

    }
}
