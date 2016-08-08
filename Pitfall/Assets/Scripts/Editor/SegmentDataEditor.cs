using UnityEngine;
using System.Collections;
using UnityEditor;

/**
 * Add a custom editor button to the SegmentData script allowing us
 * to add additional barrel spawns
 */
[CustomEditor(typeof(SegmentData))]
public class SegmentDataEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SegmentData segmentData = (SegmentData)target;
        if (GUILayout.Button("Add Spawn"))
        {
            segmentData.AddSpawn();
        }
    }
}
