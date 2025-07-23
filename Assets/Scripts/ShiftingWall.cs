using UnityEngine;
using System.Collections; // Needed for Coroutines

public class ShiftingWall : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 targetLocalPosition; // Where the wall moves TO (relative to its parent)
    public float moveDuration = 1.0f;   // How long it takes to move
    public float returnDelay = 2.0f;    // How long it stays at target before returning

    private Vector3 initialLocalPosition;
    private bool isMoving = false;

    void Start()
    {
        initialLocalPosition = transform.localPosition; // Store the wall's starting position
    }

    // Public method to start the wall's shift
    public void StartShift()
    {
        if (!isMoving)
        {
            StartCoroutine(ShiftSequence());
        }
    }

    // Coroutine to handle the movement sequence
    IEnumerator ShiftSequence()
    {
        isMoving = true;

        // Move to target position
        float timer = 0f;
        Vector3 startPos = transform.localPosition;
        while (timer < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetLocalPosition, timer / moveDuration);
            timer += Time.deltaTime;
            yield return null; // Wait for next frame
        }
        transform.localPosition = targetLocalPosition; // Ensure it reaches exact target

        // Wait at target position
        yield return new WaitForSeconds(returnDelay);

        // Return to initial position
        timer = 0f;
        startPos = transform.localPosition;
        while (timer < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(startPos, initialLocalPosition, timer / moveDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialLocalPosition; // Ensure it returns exact initial

        isMoving = false;
    }
}