using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

[CustomEditor(typeof(LeftEdgeWaypoint), true)]
public class LeftEdgeWaypointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WaypointBaseEditor.OnInspectorGUI((Waypoint)target);
    }
}
