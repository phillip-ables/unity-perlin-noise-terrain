using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public int depth = 20; //y axis
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offSetX = 100f;
    public float offSetY = 100f;

    void Start()
    {
        offSetX = Random.Range(0, 9999f);
        offSetY = Random.Range(0, 9999f);
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();//reference to terrain component to chang ethe data
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offSetX += Time.deltaTime * 5;
    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        //consist of a two deminsion float
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights ()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);//some perlin noise value
            }
        }
        return heights;
    }

    float CalculateHeight (int x, int y) //make sure to calculate the noise coordinate not as whole numbers
    {
        float xCoord = (float)x / width * scale + offSetX;
        float yCoord = (float)y / height * scale + offSetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
