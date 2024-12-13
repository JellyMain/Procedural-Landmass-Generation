using System;
using Sirenix.OdinInspector;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    [SerializeField, OnValueChanged("GenerateMap")]
    private DrawMode drawMode;

    [SerializeField, OnValueChanged("GenerateMap")]
    private int mapWidth;

    [SerializeField, OnValueChanged("GenerateMap")]
    private int mapHeight;

    [SerializeField, OnValueChanged("GenerateMap")]
    private Vector2 offset;

    [SerializeField, OnValueChanged("GenerateMap")]
    private int seed;

    [SerializeField, OnValueChanged("GenerateMap"), Range(0.1f, 100)]
    private float noiseScale;

    [SerializeField, OnValueChanged("GenerateMap"), Range(1, 20)]
    private int octaves;

    [SerializeField, OnValueChanged("GenerateMap"), Range(0, 1)]
    private float persistance;

    [SerializeField, OnValueChanged("GenerateMap"), Range(1, 10)]
    private float lacunarity;

    [SerializeField] private TerrainType[] regions;

    private MapDisplay mapDisplay;



    [Button]
    public void GenerateMap()
    {
        mapDisplay = GetComponent<MapDisplay>();

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance,
            lacunarity, offset);

        switch (drawMode)
        {
            case DrawMode.HeightMode:
                mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
                break;

            case DrawMode.ColorMode:
            {
                Color[] colorMap = new Color[mapWidth * mapHeight];

                for (int y = 0; y < mapHeight; y++)
                {
                    for (int x = 0; x < mapWidth; x++)
                    {
                        float currentHeight = noiseMap[x, y];

                        for (int i = 0; i < regions.Length; i++)
                        {
                            if (currentHeight <= regions[i].height)
                            {
                                colorMap[y * mapWidth + x] = regions[i].color;
                                break;
                            }
                        }
                    }
                }

                mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));

                break;
            }
        }
    }
}


[Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}


public enum DrawMode
{
    ColorMode,
    HeightMode
}
