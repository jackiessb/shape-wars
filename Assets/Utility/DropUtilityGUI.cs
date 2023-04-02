using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DropUtility))]
public class DropUtilityGUI : Editor {
    [Tooltip("Note: Current percentage is applied. Not always guaranteed!")]
    public bool button;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        button = GUILayout.Button("Generate Pickup");

        if (button)
        {
            DropUtility generator = (DropUtility) target;
            generator.determineDrop(Random.Range(1, 5), Random.Range(1, 5));
        }
    }
}
