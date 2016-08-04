using UnityEngine;
using System.Collections;

/**
 * Manages the movement of the player character.  Provides the ability to respond to input.
 */
public class PlayerMovement : MonoBehaviour {

    // player's sprite GameObject
    private GameObject playerSprite;

    // rigidbody component of the player
    private Rigidbody2D rigidbody2d;

    private HingeJoint2D hingejoint2d;

    // animator component for the player
    private Animator animator;

    // horizontal axis input -1.0 to 1.0
    private float horizontalAxis;

    // vertical axis input -1.0 to 1.0
    private float verticalAxis;

    

    // the direction the player is facing
    private bool facingRight = true;

    // is the player on the ground
    public bool grounded = true;

    // the transform of the object to check for collision with ground
    public Collider2D groundCheck;

    // only treat objects on this layer as ground
    public LayerMask groundMask;

    // normal movement speed
    public float speed = 4.0f;

    // is the player currently climbing
    public bool climbing = false;

    // is the player on a ladder trigger
    public bool onLadder = false;

    // previous gravity state
    private float prevGravity;

    // amount of force to apply to the player on jump
    public float jumpForce = 10.0f;

    // jump button was pressed
    private bool jumpPressed = false;

    // is the player in the midst of a jump (used to prevent slide-jumping)
    public bool jumping = false;

    // is the player swinging on a vine
    public bool swinging = false;

    // delay in seconds before next jump can be executed
    private float jumpDelay = 0.05f;

    // time of last jump
    private float lastJump = 0.0f;

    private float beganSwing = 0.0f;
    private float swingDelay = 0.05f;

    
    void Awake()
    {
        
        // initialize component references
        playerSprite = transform.Find("PlayerSprite").gameObject;
        rigidbody2d = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));

        // get the animator of the player's sprite
        animator = (Animator)playerSprite.GetComponent(typeof(Animator));
    }

    void Update()
    {
        // check if space is pressed
        jumpPressed = Input.GetAxis("Jump") > 0;

        // get the horizontal input
        horizontalAxis = Input.GetAxis("Horizontal");

        // get the vertical input
        verticalAxis = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // set animator parameters
        animator.SetFloat("XSpeed", Mathf.Abs(horizontalAxis));
        animator.SetFloat("YSpeed", rigidbody2d.velocity.y);

        CheckGrounded();
        RunHandler();
        JumpHandler();
        LadderHandler();
    }

    /**
     * Check if the player sprite meets the conditions required to be considered "grounded" (able to jump and run)
     */
    void CheckGrounded()
    {
        // get the collider that the groundCheck collider is in contact with
        Collider2D coll = Physics2D.OverlapCircle(groundCheck.bounds.center, groundCheck.bounds.extents.x, groundMask);
        if (coll)
        {
            // check if the collider we contacted is below our groundCheck collider
            // TODO: won't work for angled surfaces who's max y is not level with the ground surface
            if (coll.bounds.max.y < groundCheck.bounds.center.y)
            {
                grounded = true;
                StopJump();
                StopSwinging();
                StopClimbing();
            }

        }
    }

    /**
     * Handle horizontal movement of the character
     */
    void RunHandler()
    {
        if (grounded)
        {
            // set the horizontal velocity based on how far the H axis is pressed
            rigidbody2d.velocity = new Vector2(horizontalAxis * speed, rigidbody2d.velocity.y);
        }
        else
        {
            // allow a small amount of steering mid air
            rigidbody2d.AddForce(new Vector2(horizontalAxis * (speed / 2), rigidbody2d.velocity.y), ForceMode2D.Force);
        }

        // flip the sprite based on the direction it's moving
        if (horizontalAxis > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalAxis < 0 && facingRight)
        {
            Flip();
        }
    }

    /**
     * Handle character jumping
     */
    void JumpHandler()
    {
        // jump if jump button pressed and grounded
        if (!jumping && jumpPressed)
        {
            // stop climbing, jump off ladder
            if (climbing)
            {
                StopClimbing();
                BeginJump(horizontalAxis * speed);
            }
            // stop swinging, jump off vine
            else if (swinging)
            {
                StopSwinging();
                BeginJump(rigidbody2d.velocity.x + (horizontalAxis * speed));
            }
            // jump from grounded position
            else if (grounded)
            {
                BeginJump();
            }
        }
    }

    /**
     * Handle ladder climbing
     */
    void LadderHandler()
    {
        // if the player is on the ladder trigger, is not already climbing, and has pressed up or down begin climbing
        if (onLadder && !climbing && verticalAxis != 0.0f)
        {
            BeginClimbing();
        }
        // if the player was climbing but is no longer on the ladder, stop climbing
        if (climbing && !onLadder)
        {
            StopClimbing();
        }

        // if the player is currently climbing, move up or down based on the vertical axis input
        if (climbing)
        {
            rigidbody2d.velocity = new Vector2(0.0f, verticalAxis * speed);
        }
    }

    /**
     * Change the direction the sprite faces
     */
    void Flip()
    {
        // keep track of which way the sprite is facing
        facingRight = !facingRight;

        // change the local scale so the sprite appears to be facing the opposite direction
        Vector3 scale = playerSprite.transform.localScale;
        scale.x *= -1;
        playerSprite.transform.localScale = scale;
    }


    /**
     * Apply the jump force to the player's rigidbody and set grounded to false
     */
    void BeginJump (float horizontalForce = 0.0f)
    {
        if (!jumping && Time.time > lastJump + jumpDelay)
        {
            rigidbody2d.AddForce(new Vector2(horizontalForce, jumpForce), ForceMode2D.Impulse);
            grounded = false;
            jumping = true;
            lastJump = Time.time;
            animator.SetBool("jumping", true);
        }
        
    }

    void StopJump ()
    {
        if (jumping)
        {
            animator.SetBool("jumping", false);
            rigidbody2d.velocity = Vector2.zero;
        }
        jumping = false;
    }

    /**
     * Sets climbing state and disables rigidbody gravity, stores previous gravity state
     */
    void BeginClimbing ()
    {
        if (!climbing)
        {
            StopJump();
            climbing = true;
            grounded = false;
            prevGravity = rigidbody2d.gravityScale;
            rigidbody2d.gravityScale = 0.0f;
        }
    }

    /**
     * Set climbing state and restore gravity state
     */
    void StopClimbing ()
    {
        if (climbing)
        {
            climbing = false;
            rigidbody2d.gravityScale = prevGravity;
        }
    }

    /**
     * Sets swinging state and disables rigidbody gravity, stores previous gravity state
     */
    void BeginSwinging (Rigidbody2D vine)
    {
        if (!swinging)
        {
            beganSwing = Time.time;
            StopJump();
            swinging = true;
            grounded = false;
            prevGravity = rigidbody2d.gravityScale;
            rigidbody2d.gravityScale = 0.0f;
            rigidbody2d.freezeRotation = false;
            gameObject.AddComponent<HingeJoint2D>();
            hingejoint2d = (HingeJoint2D)GetComponent(typeof(HingeJoint2D));
            hingejoint2d.connectedBody = vine;
            hingejoint2d.autoConfigureConnectedAnchor = false;
            hingejoint2d.anchor = new Vector2(0.0f, -0.20f);
            hingejoint2d.connectedAnchor = new Vector2(0.0f, -3.22f);
            hingejoint2d.useLimits = true;
            JointAngleLimits2D limits = hingejoint2d.limits;
            if (facingRight)
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
        
    }

    /**
     * Set swinging state and restore gravity state
     */
    void StopSwinging ()
    {
        if (swinging && Time.time > (beganSwing + swingDelay))
        {
            swinging = false;
            rigidbody2d.gravityScale = prevGravity;
            rigidbody2d.rotation = 0.0f;
            rigidbody2d.freezeRotation = true;
            Destroy((HingeJoint2D)GetComponent(typeof(HingeJoint2D)));
        }
    }
}
