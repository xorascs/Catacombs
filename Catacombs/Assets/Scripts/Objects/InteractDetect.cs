using UnityEngine;

public class InteractDetect : MonoBehaviour
{
    // Reference to the Animator component of the door
    public Animator doorAnimator;
    public string requiredTag;
    public Vector2 boxSize; // Size of the box collider

    // Name of the boolean parameter in the Animator controller
    private string boolName = "OpenDoor";

    private void Update()
    {
        // Create a box collider at the center of this GameObject's position
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f);

        // Check each collider
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to an object with the required tag
            if (collider.CompareTag(requiredTag))
            {
                // Set the boolean parameter in the Animator controller to true
                doorAnimator.SetBool(boolName, true);
                return; // Exit the loop once the first object is found
            }
        }

        // If no object is found, set the boolean parameter to false
        doorAnimator.SetBool(boolName, false);
    }

    // Draw the box collider gizmo for visual debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(boxSize.x, boxSize.y, 1f));
    }
}
