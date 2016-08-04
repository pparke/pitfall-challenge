using UnityEngine;
using System.Collections;

public class GroundState : IPlayerState {

    private readonly PlayerController player;

    public GroundState(PlayerController playerController)
    {
        player = playerController;
    }

    // physics related update
    public void fixedUpdate()
    {
        if (player.CheckGrounded())
        {
            // set the horizontal velocity based on how far the H axis is pressed
            player.rigidbody2d.velocity = new Vector2(player.horizontalAxis * player.speed, player.rigidbody2d.velocity.y);
        }

        // flip the sprite based on the direction it's moving
        if (player.horizontalAxis > 0 && !player.facingRight)
        {
            player.Flip();
        }
        else if (player.horizontalAxis < 0 && player.facingRight)
        {
            player.Flip();
        }
    }

    // called every frame
    public void update()
    {
        if (player.jumpPressed)
        {
            player.ChangeState("jump");
        }
    }

    // collision with a trigger
    public void onTriggerEnter(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ladder") && player.verticalAxis != 0.0f)
        {
            player.collidedWith = coll;
            player.ChangeState("climb");
        }
        else if (coll.gameObject.CompareTag("vine"))
        {
            player.collidedWith = coll;
            player.ChangeState("swing");
        }
    }

    public void onTriggerStay(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ladder") && player.verticalAxis != 0.0f)
        {
            player.collidedWith = coll;
            player.ChangeState("climb");
        }
        else if (coll.gameObject.CompareTag("vine"))
        {
            player.collidedWith = coll;
            player.ChangeState("swing");
        }
    }

    public void onTriggerExit(Collider2D coll)
    {

    }


    // called when state entered
    public void enter()
    {
        player.horizontalForce = 0.0f;
    }

    // called before state left
    public void exit()
    {

    }
}
