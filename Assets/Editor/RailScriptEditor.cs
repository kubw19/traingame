using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RailScript))]
public class RailScriptEditor : Editor
{
    // Start is called before the first frame update
    void OnSceneGUI()
    {

        if (Event.current.commandName == "SoftDelete")
        {
            var rail = (RailScript)target;
            rail.To.Ways.Remove(rail.From);
            rail.From.Ways.Remove(rail.To);
        }
    }
}

