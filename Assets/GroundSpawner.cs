using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab; // Assign the ground prefab in the Inspector
    public Transform player; // Assign the player to track movement
    public int maxGroundTiles = 500; // Limit the number of active tiles
    public float groundLength = 1f; // Adjust based on prefab length

    private Queue<GameObject> groundTiles = new Queue<GameObject>();
    private float nextSpawnZ = 0f;

    void Start()
    {
        // Spawn initial ground tiles
        for (int i = 0; i < maxGroundTiles; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        // Check if player moved far enough to spawn new ground
        if (player.position.x > nextSpawnZ - (maxGroundTiles * groundLength))
        {
            SpawnGround();
            RemoveOldGround();
        }
    }

    void SpawnGround()
    {
        Vector3 spawnPosition = new Vector3(nextSpawnZ, player.position.y , 0); // Adjusted Y to align with player
        GameObject newGround = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
        groundTiles.Enqueue(newGround);
        nextSpawnZ += groundLength;
    }

    void RemoveOldGround()
    {
        if (groundTiles.Count > maxGroundTiles)
        {
            Destroy(groundTiles.Dequeue());
        }
    }
}
