using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Main player controller script.  Contains fields and methods used by various states
 * delegates most events to the current state.
 * Based partly on the unity tutorial for 2D character controller https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-character-controllers
 */
public class PlayerController : MonoBehaviour {

    // the current state being used
    private IPlayerState currentState;

    // the states the player will use
    private Dictionary<string, IPlayerState> states;

    // player's sprite GameObject
    private GameObject playerSprite;

    // the game manager reference
    [HideInInspector]
    public GameManager gameManager;

    // rigidbody component of the player
    [HideInInspector]
    public Rigidbody2D rigidbody2d;

    // animator component for the player
    [HideInInspector]
    public Animator animator;

    // hinge joint reference
    [HideInInspector]
    public HingeJoint2D hingejoint2d;

    // 2d collider reference
    [HideInInspector]
    public Collider2D collidedWith;

    // normal movement speed
    public float speed = 4.0f;

    // amount of force to apply to the player on jump
    public float jumpForce = 10.0f;

    // force to be added when the player holds the jump button
    public float jumpBoost = 20.0f;

    // amount of horizontal force to apply to the player as part of a jump
    public float horizontalForce = 2.0f;

    // amount of horizontal force to apply when changing direction during a jump
    public float horizontalCorrection = 0.5f;

    // jump button was pressed
    public bool jumpPressed = false;

    // the amount of time the jump button has been pressed down
    public float jumpPressedDuration;

    // horizontal axis input -1.0 to 1.0
    public float horizontalAxis;

    // vertical axis input -1.0 to 1.0
    public float verticalAxis;

    // the transform of the object to check for collision with ground
    public Collider2D groundCheck;

    // only treat objects on this layer as ground
    public LayerMask groundMask;

    // the direction the sprite is facing
    public bool facingRight = true;

    // the players score (also used as health)
    public int score = 2000;

    // the number of lives the player has left
    public int lives = 3;


    void Awake () {
        // setup states
        states = new Dictionary<string, IPlayerState>();
        states["ground"]    = new GroundState(this);
        states["jump"]      = new JumpState(this);
        states["climb"]     = new ClimbState(this);
        states["swing"]     = new SwingState(this);
        states["damage"]    = new DamageState(this);
        states["dead"]      = new DeadState(this);
        states["limbo"]     = new LimboState(this);

        // initialize component references
        playerSprite = transform.Find("PlayerSprite").gameObject;
        rigidbody2d = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));

        // get a reference to the game manager
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));

        // get the animator of the player's sprite
        animator = (Animator)playerSprite.GetComponent(typeof(Animator));
    }

    void Start ()
    {
        currentState = states["ground"];
        currentState.enter();

        gameManager.UpdateUI();
    }
	
	/**
     * Save input and update the current state
     */
	void Update () {
        // check if jump button is pressed
        if (Input.GetAxis("Jump") > 0)
        {
            // increment the jump pressed duration while the button is down
            jumpPressedDuration += Time.deltaTime;
            jumpPressed = true;
        }
        else
        {
            // reset duration if button is released
            jumpPressedDuration = 0.0f;
            jumpPressed = false;
        }

        // get the horizontal input
        horizontalAxis = Input.GetAxis("Horizontal");

        // get the vertical input
        verticalAxis = Input.GetAxis("Vertical");

        // call update on current state
        currentState.update();
    }

    /**
     * Keep x and y speed on the animator up to date and
     * call fixed update on the current state.
     */
    void FixedUpdate ()
    {
        // set animator parameters
        animator.SetFloat("XSpeed", Mathf.Abs(horizontalAxis));
        animator.SetFloat("YSpeed", rigidbody2d.velocity.y);

        // call fixedUpdate on current state
        currentState.fixedUpdate();
    }

    // Pass all trigger events to current state
    void OnTriggerEnter2D (Collider2D coll)
    {
        currentState.onTriggerEnter(coll);
    }

    void OnTriggerStay2D (Collider2D coll)
    {
        currentState.onTriggerStay(coll);
    }

    void OnTriggerExit2D (Collider2D coll)
    {
        currentState.onTriggerExit(coll);
    }

    /**
     * Change the current state, calls exit on the current state and enter on the new one
     */
    public void ChangeState (string state)
    {
        if (states.ContainsKey(state))
        {
            currentState.exit();
            currentState = states[state];
            currentState.enter();
        }
        else
        {
            Debug.Log(string.Format("Can't transition to {0}, key does not exist in states.", state));
        }
    }

    /**
     * Check if the player sprite meets the conditions required to be considered "grounded"
     */
    public bool CheckGrounded ()
    {
        // get the collider that the groundCheck collider is in contact with
        Collider2D coll = Physics2D.OverlapCircle(groundCheck.bounds.center, groundCheck.bounds.extents.x, groundMask);
        if (coll)
        {
            // check if the top of the collider we contacted is below our groundCheck collider
            // TODO: won't work for angled surfaces whos max y is not level with the ground surface
            if (coll.bounds.max.y < groundCheck.bounds.center.y)
            {
                return true;
            }
        }

        return false;
    }

    /**
     * Make the sprite face the direction it is travelling.
     */
    public void FaceForward ()
    {
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
     * Change the direction the sprite faces
     */
    public void Flip ()
    {
        // keep track of which way the sprite is facing
        facingRight = !facingRight;

        // change the local scale so the sprite appears to be facing the opposite direction
        Vector3 scale = playerSprite.transform.localScale;
        scale.x *= -1;
        playerSprite.transform.localScale = scale;
    }

    /**
     * Change to dead state
     */
    public void Kill ()
    {
        // only enter state if not already in it
        if (currentState != states["dead"])
        {
            ChangeState("dead");

            // update UI
            gameManager.UpdateUI();
        }
    }

    /**
     * Inflicts damage on the player by reducing their score.
     */
    public void TakeDamage (int amt)
    {
        // reduce player's score
        score -= amt;

        // update UI
        gameManager.UpdateUI();

        ChangeState("damage");
    }

    /**
     * Add points to the player's score
     */
    public void AddPoints (int amt)
    {
        score += amt;

        // update UI
        gameManager.UpdateUI();
    }

    /**
     * Reset the player to its beginning state
     */
    public void Reset ()
    {
        lives = 3;
        score = 2000;
        ChangeState("ground");
        gameManager.UpdateUI();
    }

    /**
     * Move the player to the given position
     */
    public void Reposition (Vector2 pos)
    {
        rigidbody2d.position = pos;
    }

    /**
     * Re-enable the player (just changes back to default state for now)
     */
    public void Revive ()
    {
        ChangeState("ground");
    }

    
}
