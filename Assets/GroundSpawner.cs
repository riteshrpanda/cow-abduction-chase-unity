using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundSpawner : MonoBehaviour
{
    public Tilemap groundTilemap; // Reference to the Tilemap
    public TileBase groundTile;   // The tile to place dynamically
    public Transform player;      // Reference to the player
    
    public Vector3Int startPosition = new Vector3Int(0, 0, 0); // Initial tile position
    public int tileSpawnDistance = 50; // How far ahead to spawn tiles
    public int tileSpawnBehind = 50; // How far behind to spawn tiles
    
    private Vector3Int lastTilePosition;

    void Start()
    {
        // Get the starting position below the player
        startPosition = groundTilemap.WorldToCell(player.position);
        startPosition.y -= 1; // Ensure tiles spawn beneath the player
        lastTilePosition = startPosition;

        SpawnInitialTiles();
    }

    void Update()
    {
        if (player.position.x > lastTilePosition.x - tileSpawnDistance)
        {
            SpawnTile();
        }
    }

    void SpawnInitialTiles()
    {
        // Spawn tiles behind the player
        Vector3Int tempPosition = startPosition;
        for (int i = 0; i < tileSpawnBehind; i++)
        {
            tempPosition.x -= 1;
            groundTilemap.SetTile(tempPosition, groundTile);
        }

        // Spawn tiles under the player
        groundTilemap.SetTile(startPosition, groundTile);

        // Spawn tiles ahead of the player
        for (int i = 0; i < tileSpawnDistance; i++)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        lastTilePosition.x += 1; // Move horizontally to the right
        groundTilemap.SetTile(new Vector3Int(lastTilePosition.x, startPosition.y, 0), groundTile); // Place tile at correct height
    }
}
