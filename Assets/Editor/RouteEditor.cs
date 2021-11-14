using Assets.Resources.SceneCreationUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteEditor : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Route editor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(RouteEditor));
    }

    void OnGUI()
    {
        var generator = GameObject.Find("Tracks").GetComponent<TracksGenerator>();
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        GuiLine();
        EditorGUILayout.LabelField("Rail generator");

            EditorGUILayout.LabelField($"From: {(RouteBuilderHelper.From == null ? "" : RouteBuilderHelper.From.ObjectName)}");

        EditorGUILayout.LabelField($"To: {(RouteBuilderHelper.To == null ? "" : RouteBuilderHelper.To.ObjectName)}");

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear"))
        {
            RouteBuilderHelper.Clear();
        }

        if (GUILayout.Button("Generate"))
        {
            RouteBuilderHelper.Generate(generator);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        EditorGUILayout.EndHorizontal();

        GuiLine();
        //if (GUILayout.Button("Re generate rails"))
        //{
        //    generator.Generate();
        //}

    }

    void GuiLine(int i_height = 1)

    {

        Rect rect = EditorGUILayout.GetControlRect(false, i_height);

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

    }
}
