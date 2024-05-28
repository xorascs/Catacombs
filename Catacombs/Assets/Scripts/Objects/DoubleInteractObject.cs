using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleInteractObject : MonoBehaviour
{
    // Reference to the Animator component of the door
    public Animator doorAnimator;
    public Transform detector1;
    public Transform detector2;
    public string requiredTag1;
    public string requiredTag2;
    public int detectVariant;
    public Vector2 boxSize; // Size of the box collider

    // Name of the boolean parameter in the Animator controller
    private string boolName = "OpenDoor";

    private void Update()
    {
        // Create a box collider at the center of this GameObject's position
        Collider2D[] colliders1 = Physics2D.OverlapBoxAll(detector1.position, boxSize, 0f);
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(detector2.position, boxSize, 0f);

        // Variables to track whether both required objects are present in each detector area
        bool object1Present1 = false;
        bool object2Present1 = false;
        bool object1Present2 = false;
        bool object2Present2 = false;

        // Check colliders detected by detector 1
        foreach (Collider2D collider in colliders1)
        {
            // Check if the collider belongs to an object with the required tags
            if (collider.CompareTag(requiredTag1))
            {
                object1Present1 = true;
            }
            else if (collider.CompareTag(requiredTag2))
            {
                object2Present1 = true;
            }
        }

        // Check colliders detected by detector 2
        foreach (Collider2D collider in colliders2)
        {
            // Check if the collider belongs to an object with the required tags
            if (collider.CompareTag(requiredTag1))
            {
                object1Present2 = true;
            }
            else if (collider.CompareTag(requiredTag2))
            {
                object2Present2 = true;
            }
        }

        // Set the boolean parameter in the Animator controller based on both objects' presence in each detector area
        bool doorOpen = false;
        switch (detectVariant)
        {
            case 4:
                doorOpen = object2Present1 && object2Present2;
                break;
            case 3:
                doorOpen = object2Present1 && object1Present2;
                break;
            case 2:
                doorOpen = object1Present1 && object2Present2;
                break;
            case 1:
                doorOpen = object1Present1 && object1Present2;
                break;
            default:
                break;
        }
        doorAnimator.SetBool(boolName, doorOpen);
    }

    // Draw the box collider gizmo for visual debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detector1.position, new Vector3(boxSize.x, boxSize.y, 1f));
        Gizmos.DrawWireCube(detector2.position, new Vector3(boxSize.x, boxSize.y, 1f));
    }
}
