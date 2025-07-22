// Basic Camera Follow Script (create new C# script named 'CameraFollow' and attach to Main Camera)
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag your Player GameObject here in the Inspector
    public Vector3 offset = new Vector3(0f, 3f, -5f); // Offset from the player
    public float smoothSpeed = 10f; // How smoothly the camera follows

    void LateUpdate() // Use LateUpdate to ensure player movement is complete
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Make the camera look at the player
    }
}
