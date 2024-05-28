using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoubleObjectLeverDoorController : MonoBehaviour
{
    // Reference to the door animator
    public Animator doorAnimator;
    public Animator leverAnimator;
    public GameObject Lever;
    public Transform detector1;
    public Transform detector2;
    public string requiredTag1;
    public string requiredTag2;
    public GameObject playerHandleDoor;
    public int detectVariant;
    public Vector2 boxSize; // Size of the box collider

    // Name of the boolean parameter in the Animator controllers
    private string doorBoolName = "OpenDoor";
    private string leverBoolName = "Activated";

    // Range within which the player can interact with the door
    private float interactionRange = 2f;

    // Flag to track if the lever is currently activated
    private bool isLeverActivated = false;

    // Flag to track if the door is open or closed
    private bool isOpen = false;

    // The initial position of the object
    private Vector3 initialPosition;

    // The position when the lever is activated
    private Vector3 activatedPosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = Lever.transform.position;
        // Calculate the activated position when the lever is activated
        activatedPosition = new Vector3(initialPosition.x - 0.26f, initialPosition.y - 0.18f, initialPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the distance between the door and the player is within the interaction range
        if (Vector2.Distance(transform.position, playerHandleDoor.transform.position) <= interactionRange)
        {
            // Check if the player presses the interaction key (e.g., "E")
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Toggle the door state
                isOpen = !isOpen;

                // Set the "OpenDoor" parameter in the door animator based on the door state
                leverAnimator.SetBool(leverBoolName, isOpen);

                // Set the lever activation state
                isLeverActivated = isOpen;

                // Move the object based on the lever activation state
                if (isLeverActivated)
                {
                    // Move the object to the activated position immediately
                    Lever.transform.position = activatedPosition;
                }
                else
                {
                    // Move the object back to its initial position immediately
                    Lever.transform.position = initialPosition;
                }
            }
        }

        // Create box colliders for both detector areas
        Collider2D[] colliders1 = Physics2D.OverlapBoxAll(detector1.position, boxSize, 0f);
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(detector2.position, boxSize, 0f);

        // Variables to track whether required objects are present in each detector area
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
                doorOpen = object2Present1 && object2Present2 && isLeverActivated;
                break;
            case 3:
                doorOpen = object2Present1 && object1Present2 && isLeverActivated;
                break;
            case 2:
                doorOpen = object1Present1 && object2Present2 && isLeverActivated;
                break;
            case 1:
                doorOpen = object1Present1 && object1Present2 && isLeverActivated;
                break;
            default:
                break;
        }

        // Set the boolean parameters in the Animator controllers
        doorAnimator.SetBool(doorBoolName, doorOpen);
    }

    // Draw the box collider gizmos for visual debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detector1.position, new Vector3(boxSize.x, boxSize.y, 1f));
        Gizmos.DrawWireCube(detector2.position, new Vector3(boxSize.x, boxSize.y, 1f));
    }
}
