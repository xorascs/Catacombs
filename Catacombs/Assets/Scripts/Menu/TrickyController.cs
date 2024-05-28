using UnityEngine;

public class TrickyController : MonoBehaviour
{
    // The object to be moved
    public Transform objectToMove;

    // Flag to determine if the object should follow the cursor
    private bool isFollowingCursor = false;

    // Original position of the object
    private Vector3 originalPosition;

    // Speed at which the object returns to its original position
    public float returnSpeed = 5f;

    void Start()
    {
        // Store the original position of the object
        originalPosition = objectToMove.position;
    }

    void Update()
    {
        // Check if the player presses the "E" button
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the follow cursor state
            isFollowingCursor = !isFollowingCursor;

            // If the object should stop following the cursor
            if (!isFollowingCursor)
            {
                // No need to reset position instantly
                // objectToMove.position = originalPosition;
            }
        }

        // If the object should follow the cursor
        if (isFollowingCursor)
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Ensure the object stays at the same z-position
            mousePosition.z = objectToMove.position.z;

            // Move the object to the mouse position smoothly
            objectToMove.position = Vector3.Lerp(objectToMove.position, mousePosition, Time.deltaTime * returnSpeed);
        }
        else
        {
            // If the object is not following the cursor, smoothly return it to the original position
            objectToMove.position = Vector3.Lerp(objectToMove.position, originalPosition, Time.deltaTime * returnSpeed);
        }
    }
}
