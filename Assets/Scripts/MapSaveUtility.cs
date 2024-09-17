using UnityEditor;
using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// Save Load Maps Tool
    /// </summary>
    public class MapSaveUtility
    {
        /// <summary>
        /// Save Map and export to the Resources Files
        /// </summary>
        /// <param name="mapObject">Map object</param>
        /// <param name="fileName">File Name</param>
        public void SaveMap(GameObject mapObject, string fileName)
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            string prefabPath = $"Assets/Resources/{fileName}.prefab";

            GameObject existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (existingPrefab != null)
            {
                PrefabUtility.SaveAsPrefabAssetAndConnect(mapObject, prefabPath, InteractionMode.UserAction);
                EditorUtility.SetDirty(existingPrefab);
            }
            else
            {
                PrefabUtility.SaveAsPrefabAsset(mapObject, prefabPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Load Map from Assets/Resources
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <returns>Map Object</returns>
        public GameObject LoadMap(string fileName)
        {
            string prefabPath = fileName.Replace(".prefab", "");
            GameObject prefab = Resources.Load<GameObject>(prefabPath);

            if (prefab != null)
            {
                return prefab;
            }
            else
            {
                Debug.LogWarning($"Map '{fileName}' could not be found in Resources.");
                return null;
            }
        }
    }
}

