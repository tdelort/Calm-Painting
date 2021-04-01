using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSideFlatter : MonoBehaviour
{
    [SerializeField] Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        int res = terrain.terrainData.heightmapResolution; 
        float[,] heights;
        heights = terrain.terrainData.GetHeights(0,0,res,res);
        for (int i = 0; i < res; i++)
            for (int j = 0; j < res; j++)
                if (i == 0 || j == 0 || i == res - 1 || j == res - 1)
                    heights[i,j] = 0;


        terrain.terrainData.SetHeights(0, 0, heights);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
