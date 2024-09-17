using UnityEngine;
using UnityEditor;

namespace MapGenerator
{
    [CustomEditor(typeof(RandomMapGenerator))]
    public class RandomMapGeneratorEditor : Editor
    {
        public string MapName = "Map";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RandomMapGenerator generator = (RandomMapGenerator)target;

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Generation"))
            {
                generator.GenerateMap();
            }

            if (GUILayout.Button("ClearMap"))
            {
                generator.ClearMap();
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginVertical();

            MapName = EditorGUILayout.TextField("Map Name", MapName);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Map"))
            {
                generator.SaveMap(MapName);
            }

            if (GUILayout.Button("Load Map"))
            {
                generator.LoadMap(MapName);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }

}