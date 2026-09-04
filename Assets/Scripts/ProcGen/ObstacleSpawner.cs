using System.Diagnostics;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameTimer timer;

    private int spawnRangeMinX = -10;
    private int spawnRangeMaxX = 10;
    private int distanceUntilObstacleSpawn;
    private float lastSpawnPoint = 0.0f;
    public float obstacleSpawnDistance = 5.0f;
    public int numObstacles = 1;
    public float spawnTimeDelay = 0.0f;
    public float spawnOriginDistance = 28.0f;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.Get_CurrentTime() >= spawnTimeDelay)
        {
            SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        /*
        distanceUntilObstacleSpawn = MathF.Floor(player.transform.position.y);

        if(distanceUntilObstacleSpawn >= obstacleSpawnDistance)
        {
            for(int i = 0; i < numObstacles; i++)
            {
                Spawn();
            }
            distanceUntilObstacleSpawn = 0.0f;
            lastSpawnPoint = player.transform.position.y;
        }
        */

        // I want to check if player has moved at least 5.0f blocks since last spawn
        if((lastSpawnPoint + obstacleSpawnDistance) <= player.transform.position.y)
        {
            for(int i = 0; i < numObstacles; i++)
            {
                Spawn();
            }
            lastSpawnPoint = player.transform.position.y;
        }

    }

    private void Spawn()
    {
        int spawnX = Random.Range(spawnRangeMinX, spawnRangeMaxX);
        float spawnY = player.transform.position.y + spawnOriginDistance;
        float spawnZ = 0;
        UnityEngine.Vector3 spawnLocation = new UnityEngine.Vector3 (spawnX, spawnY, spawnZ);
        GameObject spawnedObstacle = Instantiate(obstaclePrefab, spawnLocation, Quaternion.identity);

        //DestroyDelay(spawnedObstacle);
    }

    private void DestroyDelay(GameObject p_obstacle)
    {
        /*
        if(p_obstacle != null)
        {
            do
            {
                float destroyCheck = player.transform.position.y - spawnOriginDistance;
                if(destroyCheck > p_obstacle.transform.position.y)
                {
                    UnityEngine.Debug.Log("Destroy");
                    Destroy(p_obstacle);
                }
            } while (p_obstacle != null);
        }
        */
    }
}
