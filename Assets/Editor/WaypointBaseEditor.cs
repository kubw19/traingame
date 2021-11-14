using UnityEngine;
using UnityEditor;
using Assets.Resources.SceneCreationUtils;

public class WaypointBaseEditor
{
    public static void OnInspectorGUI(Waypoint obiekt)
    {
        Vector3 pozycja = obiekt.transform.position;
        GUILayout.Label("Pozycja globalna: ");
        float x = EditorGUILayout.FloatField("x: ", pozycja.x);

        float y = EditorGUILayout.FloatField("y: ", pozycja.y);

        float z = EditorGUILayout.FloatField("z: ", pozycja.z);

        obiekt.setGlobalPosition(new Vector3(x, y, z));

        if (!obiekt.HasReachedRailsLimit)
        {
            if (GUILayout.Button("Add to route"))
            {
                RouteBuilderHelper.AddWaypoint(obiekt);
            }
        }
        else
        {
            GUILayout.Label("Cannot add to route - rail limit reached");
        }
    }

}

[CustomEditor(typeof(Waypoint), true)]
public class WaypointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WaypointBaseEditor.OnInspectorGUI((Waypoint)target);
    }
}

