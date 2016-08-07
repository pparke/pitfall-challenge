using UnityEngine;
using System.Collections;

/**
 * Defines player behaviour when they are swinging on a vine
 * based on https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public class SwingState : IPlayerState {

    private readonly PlayerController player;

    // previous gravity state
    private float prevGravity;

    // hinge joint reference
    private HingeJoint2D hingejoint2d;

    public SwingState (PlayerController playerController)
    {
        player = playerController;
    }

    // physics related update
    public void fixedUpdate()
    {

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

    }

    public void onTriggerStay(Collider2D coll)
    {

    }

    public void onTriggerExit(Collider2D coll)
    {

    }


    /**
     * Disables gravity on the player and attaches them to the vine using a hinge joint
     */
    public void enter()
    {
        // save gravity state and disable for player
        prevGravity = player.rigidbody2d.gravityScale;
        player.rigidbody2d.gravityScale = 0.0f;

        // unfreeze rotation so player can rotate with vine
        player.rigidbody2d.freezeRotation = false;

        // add a new hinge joint to the player and connect it to the rigidbody it collided with
        player.gameObject.AddComponent<HingeJoint2D>();
        hingejoint2d = (HingeJoint2D)player.GetComponent(typeof(HingeJoint2D));
        hingejoint2d.connectedBody = (Rigidbody2D)player.collidedWith.GetComponent(typeof(Rigidbody2D));

        // setup hinge joint
        hingejoint2d.autoConfigureConnectedAnchor = false;
        hingejoint2d.anchor = new Vector2(0.0f, -0.20f);
        hingejoint2d.connectedAnchor = new Vector2(0.0f, -3.22f);

        // set limits on joint so player can swing a little but not fully rotate
        hingejoint2d.useLimits = true;
        JointAngleLimits2D limits = hingejoint2d.limits;
        if (player.facingRight)
        {
            limits.min = -10.0f;
            limits.max = 20.0f;
        }
        else
        {
            limits.min = -10.0f;
            limits.max = 20.0f;
        }

        hingejoint2d.limits = limits;
    }

    /**
     * Restores gravity and removes the hinge joint
     */
    public void exit()
    {
        // restore gravity state, set rotation back to 0 and freeze
        player.rigidbody2d.gravityScale = prevGravity;
        player.rigidbody2d.rotation = 0.0f;
        player.rigidbody2d.freezeRotation = true;

        // cleanup hinge joint
        PlayerController.Destroy((HingeJoint2D)player.GetComponent(typeof(HingeJoint2D)));
    }
}
