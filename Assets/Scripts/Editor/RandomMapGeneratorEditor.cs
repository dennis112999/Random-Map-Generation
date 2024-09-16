using UnityEngine;
using UnityEditor;

namespace MapGenerator
{
    [CustomEditor(typeof(RandomMapGenerator))]
    public class RandomMapGeneratorEditor : Editor
    {
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
        }
    }

}