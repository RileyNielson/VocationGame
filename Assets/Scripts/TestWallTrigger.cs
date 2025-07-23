using UnityEngine;

public class TestWallTrigger : MonoBehaviour
{
    public ShiftingWall[] wallsToShift; // Drag your ShiftingWall GameObjects here

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger is the player
        if (other.CompareTag("Player")) // Make sure your Player GameObject has the tag "Player"
        {
            Debug.Log("Player entered trigger! Shifting walls.");
            foreach (ShiftingWall wall in wallsToShift)
            {
                if (wall != null)
                {
                    wall.StartShift(); // Call the public method on each wall
                }
            }
        }
    }
}