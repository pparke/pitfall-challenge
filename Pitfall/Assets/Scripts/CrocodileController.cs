using UnityEngine;
using System.Collections;

public class CrocodileController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Awake () {
        animator = (Animator)GetComponent(typeof(Animator));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("closed"))
        {
            Debug.Log(coll.gameObject.name);
            if (coll.gameObject.name == "Mouth" && coll.gameObject.tag == "Player")
            {
                coll.gameObject.SendMessage("Kill");
            }
        }
        
    }
}
