using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Spawns barrels according to a timing pattern
 */
public class BarrelSpawner : MonoBehaviour {

    // the barrel object to spawn
    public GameObject barrel;

    // the timing pattern
    public List<float> pattern = new List<float>();

    // current position in list
    private int _cursor = 0;

    // reference to main camera used to position spawner
    private Camera mainCamera;

   
    // return the current pattern element and increment the cursor
    // return to start of pattern after exhausted
    private float next {
        get
        {
            if (_cursor < pattern.Count)
            {
                return pattern[_cursor++];
            }
            else
            {
                _cursor = 0;
                return pattern[_cursor++];
            }
        }
    }

    void Awake ()
    {
        mainCamera = (Camera)GameObject.Find("Main Camera").GetComponent(typeof(Camera));
    }

    // initialize
    void Start ()
    {
        MoveOffscreen();
        Invoke("SpawnBarrel", next);
    }

    // physics update
    void FixedUpdate ()
    {
        MoveOffscreen();
    }

    /**
     * Instantiate a new barrel and continue invoking itself
     * while enabled
     */
    void SpawnBarrel ()
    {
        Instantiate(barrel, transform.position, barrel.transform.rotation);
        if (this.enabled)
        {
            Invoke("SpawnBarrel", next);
        }
    }

    /**
     * Reposition the spawner so that it is always off the right hand side of the screen
     */
    void MoveOffscreen ()
    {
        // get the offscreen position for the barrel spawner
        Vector3 offscreenPos = mainCamera.ViewportToWorldPoint(new Vector3(1.4f, transform.position.y, transform.position.z));

        // keep the spawner off the right side of the viewport
        Vector3 currentPos = transform.position;
        currentPos.x = offscreenPos.x;
        transform.position = currentPos;
    }

    /**
     * Set the barrel timing pattern and kick off barrel spawning
     */
    public void SetPattern (List<float> p)
    {
        this.enabled = true;
        pattern = p;
        _cursor = 0;
        Invoke("SpawnBarrel", next);
    }

    
}
