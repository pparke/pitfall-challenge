using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarrelSpawner : MonoBehaviour {

    public GameObject barrel;
    public List<float> pattern = new List<float>();

    private Camera mainCamera;
    private int _cursor = 0;

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

    void Start ()
    {
        MoveOffscreen();
        Invoke("SpawnBarrel", next);
    }

    void FixedUpdate ()
    {
        MoveOffscreen();
    }

    void SpawnBarrel ()
    {
        Instantiate(barrel, transform.position, barrel.transform.rotation);
        if (this.enabled)
        {
            Invoke("SpawnBarrel", next);
        }
    }

    void MoveOffscreen ()
    {
        // get the offscreen position for the barrel spawner
        Vector3 offscreenPos = mainCamera.ViewportToWorldPoint(new Vector3(1.4f, transform.position.y, transform.position.z));

        // keep the spawner off the right side of the viewport
        Vector3 currentPos = transform.position;
        currentPos.x = offscreenPos.x;
        transform.position = currentPos;
    }

    
}
