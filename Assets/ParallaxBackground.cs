using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float baseSpeed = 1f; // Global base speed
    public float speedMultiplier = 1f; // Set per-layer in Inspector

    private float layerWidth;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            layerWidth = sr.bounds.size.x;
        else
            Debug.LogError($"Missing SpriteRenderer on {gameObject.name}");
    }

    void Update()
    {
        transform.position += Vector3.right * baseSpeed * speedMultiplier * Time.deltaTime;

        if (transform.position.x > layerWidth)
        {
            float leftMostX = FindLeftmostLayerX();
            transform.position = new Vector3(leftMostX - layerWidth, transform.position.y, transform.position.z);
        }
    }

    float FindLeftmostLayerX()
    {
        float minX = float.MaxValue;
        foreach (ParallaxBackground layer in FindObjectsByType<ParallaxBackground>(FindObjectsSortMode.None))
        {
            if (layer.transform.position.x < minX)
                minX = layer.transform.position.x;
        }
        return minX;
    }
}
