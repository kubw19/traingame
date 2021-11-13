﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class GlobalPosition : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Waypoint obiekt = (Waypoint)target;
        Vector3 pozycja = obiekt.transform.position;
        GUILayout.Label("Pozycja globalna: ");
        float x = EditorGUILayout.FloatField("x: ", pozycja.x);

        float y = EditorGUILayout.FloatField("y: ", pozycja.y);

        float z = EditorGUILayout.FloatField("z: ", pozycja.z);

        obiekt.setGlobalPosition(new Vector3(x, y, z));
    }
}
