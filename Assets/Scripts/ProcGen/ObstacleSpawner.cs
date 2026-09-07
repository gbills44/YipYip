using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameTimer timer;

    // Obstacle spawn vars
    private int spawnRangeMinX = -10; // based on map width
    private int spawnRangeMaxX = 10; // based on map width
    private UnityEngine.Vector3 lastSpawnPoint = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);
    public float obstacleSpawnDistanceMin = 5.0f;
    public float obstacleSpawnDistanceMax = 6.0f;
    public int numObstacles = 1;
    public float spawnTimeDelay = 0.0f;
    public float spawnOriginDistance = 28.0f;

    public float warningOffsetY = -10.0f;
    public float warningOffsetX = -1.0f;


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

        if((lastSpawnPoint.y + obstacleSpawnDistanceMin) <= player.transform.position.y)
        {
            for(int i = 0; i < numObstacles; i++)
            {
                float spawnPoint = Random.Range(obstacleSpawnDistanceMin, obstacleSpawnDistanceMax);
                //Spawn(spawnPoint);
                SpawnPrefab();
            }
            lastSpawnPoint.y = player.transform.position.y;
        }

    }

    private void SpawnPrefab()
    {
        int spawnX = 0;

        do
        {
            spawnX = Random.Range(spawnRangeMinX, spawnRangeMaxX);

        } while (spawnX == lastSpawnPoint.x);

        float spawnY = player.transform.position.y + spawnOriginDistance;
        float spawnZ = 0.0f;

        UnityEngine.Vector3 spawnLocation = new UnityEngine.Vector3 (spawnX, spawnY, spawnZ);
        GameObject spawnedObstacle = Instantiate(obstaclePrefab) as GameObject;
        spawnedObstacle.transform.position = spawnLocation;

        SpawnWarningPrefab(spawnLocation);

        UnityEngine.Debug.Log("SO-ID: " + spawnedObstacle.GetInstanceID());
        DestroyDelay(ref spawnedObstacle);
    }

    private void SpawnWarningPrefab(UnityEngine.Vector3 p_location)
    {
        UnityEngine.Vector3 spawnLocation = p_location;
        spawnLocation.y += warningOffsetY;
        spawnLocation.x += warningOffsetX;

        GameObject spawnedWarning = Instantiate(warningPrefab) as GameObject;
        spawnedWarning.transform.position = spawnLocation;
    }

    // Deprecated spawn function
/*
    private void Spawn(float p_spawnPoint)
    { 
        int spawnX = 0;
        
        do
        {
            spawnX = Random.Range(spawnRangeMinX, spawnRangeMaxX);

        } while (spawnX == lastSpawnPoint.x);

        float spawnY = player.transform.position.y + spawnOriginDistance;
        float spawnZ = 0;
        UnityEngine.Vector3 spawnLocation = new UnityEngine.Vector3 (spawnX, spawnY, spawnZ);
        GameObject spawnedObstacle = Instantiate(obstaclePrefab, spawnLocation, Quaternion.identity);
        spawnedObstacle.transform.position = spawnLocation;
        DestroyDelay(ref spawnedObstacle);
    }
    */

    private void DestroyDelay(ref UnityEngine.GameObject p_obstacle)
    {
        UnityEngine.Debug.Log("Destroy Delay");
        if(p_obstacle != null)
        {
            UnityEngine.Debug.Log("p_obstacle non null");
        }
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
