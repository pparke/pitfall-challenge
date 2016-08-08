using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Contains data and methods pertaining to the current level segment
 */
public class SegmentData : MonoBehaviour {

    // the timing pattern for barrel spawning
    public List<float> barrelPattern = new List<float>();

    // new time interval to add when Add Spawn is clicked in the editor
    public float newSpawn;

    // is this the active segment?
    private bool active = false;

    // the barrel spawner to use
    private BarrelSpawner barrelSpawn;

    public void Awake ()
    {
        // get the global barrel spawner
        barrelSpawn = (BarrelSpawner)GameObject.Find("BarrelSpawn").GetComponent(typeof(BarrelSpawner));
    }

    /**
     * Setup the segment
     */
    void Enter ()
    {
        if (barrelPattern.Count > 0)
        {
            barrelSpawn.SetPattern(barrelPattern);
        }
        else
        {
            barrelSpawn.enabled = false;
        }
    }

    /**
     * Shutdown the segment
     */
    void Exit ()
    {
        barrelSpawn.enabled = false;
    }

    /**
     * Toggle the active state of the segment and trigger the entry or exit code
     */
    public void Toggle ()
    {
        active = !active;
        if (active)
        {
            Enter();
        }
        else
        {
            Exit();
        }
    }

    /**
     * Add a new barrel spawn to this segment's pattern
     */
    public void AddSpawn ()
    {
        barrelPattern.Add(newSpawn);
    }
}
