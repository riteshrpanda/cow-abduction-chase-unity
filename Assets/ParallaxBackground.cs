using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect;
    private Transform cam;
    private Vector3 lastCamPos;
    private float backgroundWidth;
    public float loopOffset = 5f; // Adjust this to control how early the background appears

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float deltaX = cam.position.x - lastCamPos.x;
        transform.position += Vector3.right * (deltaX * parallaxEffect);
        lastCamPos = cam.position;

        // Make the background appear earlier
        if (cam.position.x - transform.position.x >= backgroundWidth - loopOffset)
        {
            transform.position += Vector3.right * backgroundWidth;
        }
    }
}
