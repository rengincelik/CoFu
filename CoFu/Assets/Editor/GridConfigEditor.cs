#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridConfig))]
public class GridConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {



        // Ensure that any changes made in the inspector fields are serialized
        serializedObject.Update(); 
        
        GridConfig config = (GridConfig)target;
        if (config.spawnPattern == null || config.spawnPattern.Length != config.gridWidth * config.gridHeight)
        {
            System.Array.Resize(ref config.spawnPattern, config.gridWidth * config.gridHeight);
        }
        // Boyut ayarları
        EditorGUILayout.LabelField("Grid Settings", EditorStyles.boldLabel);
        
        // Use SerializedProperty for the width/height to work better with Undo/Redo
        SerializedProperty widthProp = serializedObject.FindProperty("gridWidth");
        SerializedProperty heightProp = serializedObject.FindProperty("gridHeight");

        EditorGUILayout.PropertyField(widthProp, new GUIContent("Width"));
        EditorGUILayout.PropertyField(heightProp, new GUIContent("Height"));

        EditorGUILayout.Space();

        // --- Grid çizimi ---
        EditorGUILayout.LabelField("Spawn Pattern", EditorStyles.boldLabel);

        // CRITICAL BOUNDS CHECK: Ensure the array is valid before drawing the grid
        int expectedSize = config.gridWidth * config.gridHeight;
        if (config.spawnPattern == null || config.spawnPattern.Length != expectedSize)
        {
            // If the array is out of sync, trigger OnValidate immediately 
            // and show a message while the array is being resized.
            config.OnValidate(); 
            EditorUtility.SetDirty(config); 
            
            EditorGUILayout.HelpBox("Grid is resizing...", MessageType.Warning);
            // Don't draw the grid until the next frame when the array is safe.
        }
        else
        {
            // Drawing the grid is now safe
            DrawGridButtons(config, expectedSize);
        }
        
        // Apply changes from PropertyFields and trigger OnValidate if fields changed
        serializedObject.ApplyModifiedProperties(); 

        // Stats
        // ... (Stats calculation remains the same, placed after drawing the grid)
        int activeCount = 0;
        foreach (bool b in config.spawnPattern)
            if (b) activeCount++;

        EditorGUILayout.HelpBox(
            $"Active Spawn Points: {activeCount}/{config.spawnPattern.Length}",
            MessageType.Info
        );

        // Diğer ayarlar
        EditorGUILayout.Space();
        // DrawDefaultInspector(); // Custom inspector'da genellikle bu satırı çıkarırız
    }
    
    // Helper method to keep OnInspectorGUI clean
    private void DrawGridButtons(GridConfig config, int totalSize)
    {
        float buttonSize = 30f;

        for (int y = 0; y < config.gridHeight; y++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < config.gridWidth; x++)
            {
                int index = y * config.gridWidth + x;

                // Index is safe due to the check in OnInspectorGUI
                bool isActive = config.spawnPattern[index];
                Color oldColor = GUI.backgroundColor;
                GUI.backgroundColor = isActive ? Color.green : Color.gray;

                if (GUILayout.Button(
                    isActive ? "■" : "□",
                    GUILayout.Width(buttonSize),
                    GUILayout.Height(buttonSize)))
                {
                    // Undo is essential for user safety
                    Undo.RecordObject(config, "Toggle Spawn Cell");
                    config.spawnPattern[index] = !isActive;
                    // Ensure the change is saved
                    EditorUtility.SetDirty(config); 
                }

                GUI.backgroundColor = oldColor;
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif
