using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

    private Camera mainCamera;

    public bool rolling = true;

    private Rigidbody2D rigidbody2d;

    public float speed = -4.0f;

    // how far offscreen should the barrel be before destroying it (in viewport coords)
    public float offscreenLeft = -0.25f;

    // animator component for the barrel
    private Animator animator;

    void Awake ()
    {
        mainCamera = (Camera)GameObject.Find("Main Camera").GetComponent(typeof(Camera));
        rigidbody2d = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        // get the animator of the barrel's sprite
        animator = (Animator)GetComponent(typeof(Animator));
    }

	// Use this for initialization
	void Start () {
        animator.SetBool("rolling", rolling);
    }

    void FixedUpdate ()
    {
        if (rolling)
        {
            // set the horizontal velocity of the barrel
            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);

            // destroy if offscreen
            if (IsOffLeftSide())
            {
                Destroy(gameObject);
            }
        }
        
    }
	

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TakeDamage", 1);
        }
    }

    bool IsOffLeftSide ()
    {
        // get the position of the barrel in viewport coords
        Vector3 pos = mainCamera.WorldToViewportPoint(transform.position);
        Debug.Log(pos.x);
        return pos.x <= offscreenLeft;
    }
}
