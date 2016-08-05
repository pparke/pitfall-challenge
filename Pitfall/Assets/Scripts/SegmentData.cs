using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentData : MonoBehaviour {

    public List<float> barrelPattern = new List<float>();
    public float newSpawn;

    private BarrelSpawner barrelSpawn;

    public void Awake ()
    {
        // get the global barrel spawner
        barrelSpawn = (BarrelSpawner)GameObject.Find("BarrelSpawn").GetComponent(typeof(BarrelSpawner));
    }

    public void Enter ()
    {
        if (barrelPattern.Count > 0)
        {
            barrelSpawn.enabled = true;
            barrelSpawn.pattern = barrelPattern;
        }
        else
        {
            barrelSpawn.enabled = false;
        }
    }


    public void Exit ()
    {
        barrelSpawn.enabled = false;
    }

    public void AddSpawn ()
    {
        barrelPattern.Add(newSpawn);
    }
}
