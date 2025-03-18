using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Texture2D heightMap;
    public Texture2D normalMap;
    public float terrainLength = 100f;
    public float terrainWidth = 100f;
    public float heightMultiplier = 10f;
    public Material terrainMaterial;
    private Terrain terrain;

    void Start()
    {
        // 获取地形组件
        terrain = GetComponent<Terrain>();
        if (terrain == null)
        {
            terrain = gameObject.AddComponent<Terrain>();
            gameObject.AddComponent<TerrainCollider>();
        }

        // 设置地形数据
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = heightMap.width;
        terrainData.size = new Vector3(terrainLength, heightMultiplier, terrainWidth);

        // 从高度图设置高度数据
        float[,] heights = new float[heightMap.width, heightMap.height];
        for (int x = 0; x < heightMap.width; x++)
        {
            for (int y = 0; y < heightMap.height; y++)
            {
                heights[x, y] = heightMap.GetPixel(x, y).r;
            }
        }
        terrainData.SetHeights(0, 0, heights);

        // 这里法线图暂时仅作展示，Unity地形组件本身不直接使用法线图
        // 可以在后续的着色器中使用法线图来模拟细节
        // 示例：将法线图设置为地形材质的法线贴图
        terrain.materialTemplate = terrainMaterial;

        // 应用地形数据
        terrain.terrainData = terrainData;
    }
}