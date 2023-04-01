using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generate))]
public class GenerateGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Test Weapon"))
        {
            Generate generator = (Generate) target;
            generator.accessDebug();
        }
    }
}
