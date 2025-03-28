using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject trapSpikePrefab;  // Air spike
    public GameObject spikePrefab;      // Ground spike
    public GameObject spearPrefab;      // Surprise spear
    public Transform player;            // Player reference

    public float spawnDistance = 10f;
    public float groundLevel = -3f;
    public float jumpHeight = 2.5f;
    public float midAirHeight = 4f;
    public float spawnRate = 2f;

    private GameObject[] obstacles;

    void Start()
    {
        obstacles = new GameObject[] { trapSpikePrefab, spikePrefab, spearPrefab };
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        GameObject obstacleToSpawn = obstacles[Random.Range(0, obstacles.Length)];
        Vector3 spawnPosition = Vector3.zero;

        if (obstacleToSpawn == trapSpikePrefab)
        {
            spawnPosition = new Vector3(player.position.x + spawnDistance, jumpHeight, 0f);
        }
        else if (obstacleToSpawn == spikePrefab)
        {
            spawnPosition = new Vector3(player.position.x + spawnDistance, groundLevel+0.3f, 0f);
        }
        else if (obstacleToSpawn == spearPrefab)
        {
            spawnPosition = new Vector3(player.position.x + spawnDistance, groundLevel+0.3f, 0f); // Spear at ground level
        }

        Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
    }
}
