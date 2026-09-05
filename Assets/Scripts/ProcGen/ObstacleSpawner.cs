using System.Diagnostics;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameTimer timer;

    // based on map width
    private int spawnRangeMinX = -10;
    private int spawnRangeMaxX = 10;
    private UnityEngine.Vector3 lastSpawnPoint = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);
    public float obstacleSpawnDistanceMin = 5.0f;
    public float obstacleSpawnDistanceMax = 6.0f;
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

        if((lastSpawnPoint.y + obstacleSpawnDistanceMin) <= player.transform.position.y)
        {
            for(int i = 0; i < numObstacles; i++)
            {
                float spawnPoint = Random.Range(obstacleSpawnDistanceMin, obstacleSpawnDistanceMax);
                Spawn(spawnPoint);
            }
            lastSpawnPoint.y = player.transform.position.y;
        }

    }

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
