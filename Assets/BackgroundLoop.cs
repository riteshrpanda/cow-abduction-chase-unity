using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public GameObject[] backgrounds; // Assign 2 background prefabs in the Inspector
    private float bgWidth;
    private int nextIndex = 0;

    void Start()
    {
        // Get the width of the background (from the first child layer)
        SpriteRenderer sr = backgrounds[0].GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            bgWidth = sr.bounds.size.x;
        else
            Debug.LogError("Missing SpriteRenderer on background prefab!");

        // Position second background right next to the first one
        backgrounds[1].transform.position = new Vector3(backgrounds[0].transform.position.x + bgWidth, backgrounds[0].transform.position.y, backgrounds[0].transform.position.z);
    }

    void Update()
    {
        // Check if the player has moved past an entire background width
        if (player.position.x > backgrounds[nextIndex].transform.position.x + bgWidth)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        // Move the background that just went out of view to the front
        int previousIndex = nextIndex;
        nextIndex = (nextIndex + 1) % backgrounds.Length; // Toggle between 0 and 1

        backgrounds[previousIndex].transform.position = new Vector3(backgrounds[nextIndex].transform.position.x + bgWidth, backgrounds[nextIndex].transform.position.y, backgrounds[nextIndex].transform.position.z);
    }
}
