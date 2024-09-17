# Random-Map-Generation

A Unity-based procedural map generator that generates 3D cube-based terrain using Perlin Noise or random height generation. This tool allows you to create random maps, save them as prefabs, and load them for future use.



https://github.com/user-attachments/assets/c4865865-5530-4547-8d32-d1f8d1d6b2fe



# Features
- Procedural map generation using Perlin Noise or random heights.
- Supports configurable map size, height, and smoothness.
- Dynamically generates cube-based terrain with varying heights and colors.
- Option to save generated maps as prefabs for future use.
- Load saved maps from the Resources folder.
- Editor tools for map generation, clearing, saving, and loading.

# Getting Started
1. Requirements
   Unity 2020 or higher
   Universal Render Pipeline (Optional)
2. Installation
   Clone or download the repository.
   Open the project in Unity.
   Add the RandomMapGenerator script to an empty GameObject in your scene.
3. Usage
   
  a. Generating a Map

    Select the GameObject that has the RandomMapGenerator component attached.
    Configure the LevelConfig in the inspector, adjusting width, depth, maximum height, Perlin noise settings, and smoothness.
    Click the Generate Map button in the inspector to generate a random map based on the current configuration.
  
  b. Saving a Map

    After generating a map, you can save it as a prefab for future use.
    Enter a filename in the save field and click Save Map.
    The map will be saved to the Resources folder as a prefab.
  
  c. Loading a Map

    Use the Load Map button to load a previously saved map from the Resources folder.
  
  d. Clearing the Map

    To clear the current generated map, click the Clear Map button in the inspector.
