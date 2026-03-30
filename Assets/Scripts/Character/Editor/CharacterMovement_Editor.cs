using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterMovement))]
[CanEditMultipleObjects]
public class CharacterMovement_Editor : Editor
{

    public override void OnInspectorGUI()
    {
        CharacterMovement myTarget = (CharacterMovement)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Climb Stair"))
        {
            myTarget.ClimbStairs();
        }

    }
}
