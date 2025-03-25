using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public float spawnRate = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
{
    if (obstacles.Length == 0)
    {
        Debug.LogError("No obstacles assigned in ObstacleSpawner!");
        return;
    }

    int randIndex = Random.Range(0, obstacles.Length);
    
    // Get current spawn position
    Vector3 spawnPos = transform.position;
    
    // Randomize the X position within a range
    float randomX = Random.Range(-10f, 10f);  // Adjust range based on level size
    spawnPos.x += randomX;

    GameObject spawnedObstacle = Instantiate(obstacles[randIndex], spawnPos, Quaternion.identity);
    
    Debug.Log($"Spawned: {spawnedObstacle.name} at {spawnPos}");
}

}
