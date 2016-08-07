using UnityEngine;
using System.Collections;

/**
 * Disables physics and display on the player
 * based on https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public class LimboState : IPlayerState {

    private readonly PlayerController player;

    // previous gravity state
    private float prevGravity;

    public LimboState(PlayerController playerController)
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
     * Disable physics and display.  Reset the player to previous checkpoint
     * or reset the game if no lives remaining.
     */
    public void enter()
    {
        player.animator.SetBool("limbo", true);
        // stop displaying the player
        player.transform.Find("PlayerSprite").GetComponent<SpriteRenderer>().enabled = false;
        // save the player's gravity state and disable gravity
        prevGravity = player.rigidbody2d.gravityScale;
        player.rigidbody2d.gravityScale = 0.0f;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<CircleCollider2D>().enabled = false;
        player.rigidbody2d.velocity = Vector2.zero;

        // reset the player or the game
        if (player.lives <= 0)
        {
            player.gameManager.Reset();
        }
        else
        {
            player.gameManager.ResetPlayer();
        }
    }

    /**
     * Re-enable physics and display.
     */
    public void exit()
    {
        player.rigidbody2d.gravityScale = prevGravity;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<CircleCollider2D>().enabled = true;
        player.transform.Find("PlayerSprite").GetComponent<SpriteRenderer>().enabled = true;
        player.animator.SetBool("limbo", false);
    }
}
