using UnityEngine;
using System.Collections;

public class JumpState : IPlayerState {

    private readonly PlayerController player;

    // delay in seconds before next jump can be executed
    private float jumpDelay = 0.05f;
    // delay before exit can be called
    private float exitDelay = 0.05f;

    // time of last jump
    private float lastJump = 0.0f;

    public JumpState(PlayerController playerController)
    {
        player = playerController;
    }


    // physics related update
    public void fixedUpdate()
    {
        if (Time.time > lastJump + exitDelay && player.CheckGrounded())
        {
            player.ChangeState("ground");
        }
    }

    // called every frame
    public void update()
    {

    }

    // collision with a trigger
    public void onTriggerEnter(Collider2D coll)
    {
        Debug.Log(coll.gameObject.tag);
        Debug.Log(coll.gameObject.CompareTag("ladder"));
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
        player.rigidbody2d.AddForce(new Vector2(player.horizontalForce, player.jumpForce), ForceMode2D.Impulse);
        player.animator.SetBool("jumping", true);
        lastJump = Time.time;
    }

    // called before state left
    public void exit()
    {
        player.animator.SetBool("jumping", false);
        player.rigidbody2d.velocity = Vector2.zero;
    }
}
