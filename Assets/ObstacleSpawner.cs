using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject trapSpikePrefab;  
    public GameObject spikePrefab;      
    public GameObject spearPrefab;      
    public Transform player;            

    public float spawnDistance = 15f;
    public float groundLevel = -4f;
    public float jumpHeight = 1f;
    public float midAirHeight = 4f;
    public float spawnRate = 0.8f;  
    private float minSpawnRate = 0.001f;  
    private float difficultyIncreaseRate = 0.001f;  
    private float timeElapsed = 0f;

    private GameObject[] obstacles;

    void Start()
    {
        obstacles = new GameObject[] { trapSpikePrefab, spikePrefab, spearPrefab };
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is missing!");
            return;
        }

        GameObject obstacleToSpawn = obstacles[Random.Range(0, obstacles.Length)];
        Vector3 spawnPosition = Vector3.zero;

        if (obstacleToSpawn == trapSpikePrefab)
        {
            spawnPosition = new Vector3(player.position.x + spawnDistance, jumpHeight, 0f);
        }
        else if (obstacleToSpawn == spikePrefab || obstacleToSpawn == spearPrefab)
        {
            spawnPosition = new Vector3(player.position.x + spawnDistance, groundLevel + 0.3f, 0f);
        }

        Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned: " + obstacleToSpawn.name + " at " + spawnPosition);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (spawnRate > minSpawnRate)
        {
            float newSpawnRate = Mathf.Max(minSpawnRate, 2f - difficultyIncreaseRate * timeElapsed);

            if (Mathf.Abs(newSpawnRate - spawnRate) > 0.01f) 
            {
                spawnRate = newSpawnRate;
                RestartSpawning();
            }
        }

        Debug.Log("Current Spawn Rate: " + spawnRate);
    }

    void RestartSpawning()
    {
        CancelInvoke(nameof(SpawnObstacle));
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnRate);
        Debug.Log("Restarted spawning with rate: " + spawnRate);
    }
}
