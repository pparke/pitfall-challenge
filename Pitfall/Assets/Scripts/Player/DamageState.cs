using UnityEngine;
using System.Collections;

public class DamageState : IPlayerState {

    private readonly PlayerController player;

    private float enterTime;
    private float exitDelay = 0.25f;

    public DamageState (PlayerController playerController)
    {
        player = playerController;
    }


    // physics related update
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

    // called when state entered
    public void enter ()
    {
        player.animator.SetBool("damage", true);
        enterTime = Time.time;
        player.rigidbody2d.velocity = Vector2.zero;
    }

    // called before state left
    public void exit ()
    {
        Debug.Log("leaving damage state");
        player.animator.SetBool("damage", false);
        
    }
}
