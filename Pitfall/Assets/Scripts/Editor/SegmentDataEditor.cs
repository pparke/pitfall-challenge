using UnityEngine;
using System.Collections;
using UnityEditor;


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
