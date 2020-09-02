using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorPallet))]
public class ColorPalleteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColorPallet colorPallet = (ColorPallet)target;
        if (GUILayout.Button("Generate Color"))
        {
            colorPallet.Generate();
        }
    }
}
