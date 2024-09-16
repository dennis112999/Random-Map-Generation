using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class LevelConfig
    {
        public int Width;
        public int Depth;
        public float MapSize;
        public float SeedX;
        public float SeedZ;
        public float Relief;
        public float MaxHeight;

        public bool IsPerlinNoiseMap;
        public bool IsSmoothness;
        public bool NeedCollider;
    }

    /// <summary>
    /// 
    /// </summary>
    public class RandomMapGenerator : MonoBehaviour
    {
        public LevelConfig currentLevelConfig;

        #region MonoBehaviour

        private void Awake()
        {
            GenerateMap(currentLevelConfig);
        }

        private void OnValidate()
        {
            //if (!Application.isPlaying) return;

            transform.localScale = new Vector3(currentLevelConfig.MapSize, currentLevelConfig.MapSize, currentLevelConfig.MapSize);

            foreach (Transform child in transform)
            {
                SetCubeY(child.gameObject, currentLevelConfig);
            }
        }

        #endregion MonoBehaviour

        /// <summary>
        /// Generate Map (Init)
        /// </summary>
        /// <param name="config"></param>
        private void GenerateMap(LevelConfig config)
        {
            transform.localScale = new Vector3(config.MapSize, config.MapSize, config.MapSize);

            for (int x = 0; x < config.Width; x++)
            {
                for (int z = 0; z < config.Depth; z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.SetParent(transform, false);
                    cube.transform.localPosition = new Vector3(x, 0, z);

                    if (!config.NeedCollider)
                    {
                        Destroy(cube.GetComponent<BoxCollider>());
                    }

                    SetCubeY(cube, config);
                }
            }
        }

        /// <summary>
        /// Set the cube Y-Pos
        /// </summary>
        /// <param name="cube">Need to change cube</param>
        /// <param name="config">Level Config</param>
        private void SetCubeY(GameObject cube, LevelConfig config)
        {
            float y = CalculateHeight(cube, config);
            y = ApplySmoothness(y, config);

            SetCubePosition(cube, y);
            SetCubeColor(cube, y, config);
        }

        /// <summary>
        /// Calculate Height with IsPerlinNoiseMap or not
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="config">Level Config</param>
        /// <returns></returns>
        private float CalculateHeight(GameObject cube, LevelConfig config)
        {
            if (config.IsPerlinNoiseMap)
            {
                return CalculatePerlinNoiseHeight(
                    cube.transform.localPosition.x,
                    cube.transform.localPosition.z,
                    config
                );
            }

            return GenerateRandomHeight(config);
        }

        /// <summary>
        /// Calculate Perlin Noise Height
        /// </summary>
        /// <param name="x">cube X pos</param>
        /// <param name="z">cube z pos</param>
        /// <param name="config">Level Config</param>
        /// <returns></returns>
        private float CalculatePerlinNoiseHeight(float x, float z, LevelConfig config)
        {
            float xSample = (x + config.SeedX) / config.Relief;
            float zSample = (z + config.SeedZ) / config.Relief;
            float noise = Mathf.PerlinNoise(xSample, zSample);
            return config.MaxHeight * noise;
        }

        /// <summary>
        /// Generate Random Height
        /// </summary>
        /// <param name="config">Level Config</param>
        /// <returns>Random Height</returns>
        private float GenerateRandomHeight(LevelConfig config)
        {
            return Random.Range(0, config.MaxHeight);
        }

        /// <summary>
        /// Apply Map Smoothness
        /// </summary>
        /// <param name="height">Cube height</param>
        /// <param name="config"></param>
        /// <returns></returns>
        private float ApplySmoothness(float height, LevelConfig config)
        {
            if (!config.IsSmoothness)
            {
                return Mathf.Round(height);
            }
            return height;
        }

        /// <summary>
        /// Set Cube Position
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="y"></param>
        private void SetCubePosition(GameObject cube, float y)
        {
            cube.transform.localPosition = new Vector3(cube.transform.localPosition.x,
                                                       y,
                                                       cube.transform.localPosition.z);
        }

        /// <summary>
        /// Sets the color of the cube based on its height. 
        /// The color transitions smoothly from red (low height) to green (high height).
        /// </summary>
        /// <param name="cube">The cube GameObject whose color will be set.</param>
        /// <param name="height">The height of the cube used to determine its color.</param>
        /// <param name="config">The LevelConfig object containing the maxHeight value.</param>
        private void SetCubeColor(GameObject cube, float height, LevelConfig config)
        {
            float ratio = Mathf.Clamp01(height / config.MaxHeight);
            Color color = Color.Lerp(Color.red, Color.green, ratio);
            cube.GetComponent<MeshRenderer>().material.color = color;
        }

    }
}
