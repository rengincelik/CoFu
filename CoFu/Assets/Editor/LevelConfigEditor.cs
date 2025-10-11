#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfig))]
public class LevelConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelConfig config = (LevelConfig)target;
        
        // Boyut ayarları
        EditorGUILayout.LabelField("Grid Settings", EditorStyles.boldLabel);
        config.gridWidth = EditorGUILayout.IntField("Width", config.gridWidth);
        config.gridHeight = EditorGUILayout.IntField("Height", config.gridHeight);
        
        EditorGUILayout.Space();
        
        // Grid çizimi
        EditorGUILayout.LabelField("Spawn Pattern", EditorStyles.boldLabel);
        
        float buttonSize = 30f;
        
        for (int y = 0; y < config.gridHeight; y++)
        {
            EditorGUILayout.BeginHorizontal();
            
            for (int x = 0; x < config.gridWidth; x++)
            {
                int index = y * config.gridWidth + x;
                
                // Toggle button
                bool isActive = config.spawnPattern[index];
                Color oldColor = GUI.backgroundColor;
                GUI.backgroundColor = isActive ? Color.green : Color.gray;
                
                if (GUILayout.Button(
                    isActive ? "■" : "□",
                    GUILayout.Width(buttonSize),
                    GUILayout.Height(buttonSize)))
                {
                    config.spawnPattern[index] = !isActive;
                    EditorUtility.SetDirty(config);
                }
                
                GUI.backgroundColor = oldColor;
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space();
        
        // Stats
        int activeCount = 0;
        foreach (bool b in config.spawnPattern)
            if (b) activeCount++;
        
        EditorGUILayout.HelpBox(
            $"Active Spawn Points: {activeCount}/{config.spawnPattern.Length}",
            MessageType.Info
        );
        
        // Diğer ayarlar
        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}
#endif
