using System;
using Sirenix.OdinInspector;
using UnityEngine;


public class MapGenerator: MonoBehaviour
{
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private float noiseScale;
    private MapDisplay mapDisplay;
    
    
    
    [Button]
    public void GenerateMap()
    {
        mapDisplay = GetComponent<MapDisplay>();
        
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);
        mapDisplay.DrawNoiseMap(noiseMap);
    }
}