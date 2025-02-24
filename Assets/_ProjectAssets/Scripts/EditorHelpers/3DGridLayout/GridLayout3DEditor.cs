using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid3DLayoutComponent))]
public class GridLayout3DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Grid3DLayoutComponent gridLayout3D = (Grid3DLayoutComponent)target;
        
        if (GUILayout.Button("Generate grid"))
        {
            gridLayout3D.GenerateGrid();
        }
    }
}