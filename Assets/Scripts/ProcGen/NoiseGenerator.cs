using UnityEngine;
using UnityEngine.UIElements;

public class NoiseGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float[,] Generate(int width, int height, float scale, Wave[] waves,
    Vector2 offset)
    {
        float[,] noiseMap = new float[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; ++y)
            {
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;

                float normalization = 0.0f;

                // loop through waves
                foreach(Wave w in waves)
                {
                    noiseMap[x,y] += w.amplitude * Mathf.PerlinNoise(samplePosX * w.frequency + w.seed, 
                    samplePosY * w.frequency + w.seed);
                }

                noiseMap[x,y] /= normalization;
            }
        }

        return noiseMap;
    }
}

[System.Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}
