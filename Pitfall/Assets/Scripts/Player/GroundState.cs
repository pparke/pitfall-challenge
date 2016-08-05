using UnityEngine;
using System.Collections;

public class GroundState : IPlayerState {

    private readonly PlayerController player;

    private float moveDuration = 0.0f;
    private float runTime = 1.0f;
    private float runBoost = 2.0f;
    private float currentSpeed;

    public GroundState(PlayerController playerController)
    {
        player = playerController;
    }

    // physics related update
    public void fixedUpdate()
    {
        if (player.CheckGrounded())
        {
            // calculate current speed based on horizontal input
            currentSpeed = player.horizontalAxis * player.speed;

            // keep track of how long the player has been moving
            if (player.horizontalAxis != 0.0f)
            {
                moveDuration += Time.deltaTime;
            }
            // if the player stops moving reset the move duration
            else
            {
                moveDuration = 0.0f;
            }

            // if the player has been moving long enough, add a boost to speed
            if (moveDuration >= runTime)
            {
                currentSpeed += Mathf.Sign(currentSpeed) * runBoost;
            }

            // set the horizontal velocity based on how far the H axis is pressed
            player.rigidbody2d.velocity = new Vector2(currentSpeed, player.rigidbody2d.velocity.y);
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
