using UnityEngine;
using System.Collections;

/**
 * Defines player behaviour during a jump
 * based on unity tutorial https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public class JumpState : IPlayerState {

    private readonly PlayerController player;

    // delay before exit can be called
    private float exitDelay = 0.05f;

    // time of last jump
    private float lastJump = 0.0f;

    // has the player used their jump boost
    private bool boostUsed = false;

    public JumpState(PlayerController playerController)
    {
        player = playerController;
    }


    // physics related update
    public void fixedUpdate()
    {
        if (!player.CheckGrounded())
        {
            // calculate target speed based on horizontal input
            float xChange = player.horizontalAxis * player.horizontalCorrection;
            float maxVel = player.horizontalForce + player.speed;
            float xComponent = Mathf.Clamp(player.rigidbody2d.velocity.x + xChange, -maxVel, maxVel);

            // set the horizontal velocity based on how far the H axis is pressed
            player.rigidbody2d.velocity = new Vector2(xComponent, player.rigidbody2d.velocity.y);
            

            // increase jump force when jump is held down
            if (!boostUsed && player.jumpPressedDuration > 0.15)
            {
                float yComponent = player.jumpBoost * player.jumpPressedDuration;
                player.rigidbody2d.AddForce(new Vector2(0.0f, yComponent), ForceMode2D.Impulse);
                boostUsed = true;
            }
        }
        // prevent player from exiting jump state too early when leaving the ground
        else if (Time.time > lastJump + exitDelay)
        {
            player.ChangeState("ground");
        }

        // flip the sprite based on the direction it's moving
        player.FaceForward();
    }

    // called every frame
    public void update()
    {

    }

    /**
     * The player can change to the climbing or swinging state after entering a trigger.
     */
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

    /**
     * Add an impulse force to the player based on the horizontal axis and how long
     * the jump button has been held down.
     */
    public void enter()
    {
        boostUsed = false;

        float xComponent = player.horizontalForce * player.horizontalAxis;
        player.rigidbody2d.AddForce(new Vector2(xComponent, player.jumpForce), ForceMode2D.Impulse);
        player.animator.SetBool("jumping", true);
        lastJump = Time.time;
    }

    /**
     * Stop the jump animation and reset
     * as well as come to a full stop
     */
    public void exit()
    {
        player.animator.SetBool("jumping", false);
        player.jumpPressedDuration = 0.0f;
        player.rigidbody2d.velocity = Vector2.zero;
    }
}
