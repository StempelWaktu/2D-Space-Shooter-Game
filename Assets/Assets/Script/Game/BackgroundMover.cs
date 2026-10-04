using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float scrollSpeed = 2.0f;
    public float resetPosition = -10f; // Posisi untuk reset background
    public float startPosition = 10f;  // Posisi awal background

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= resetPosition)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
