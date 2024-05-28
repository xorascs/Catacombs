using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MultiLevController : MonoBehaviour
{
    public MultiLeverDoorController doorController;
    public Animator leverAnimator;
    public int leverIndex; // Index of the lever in the door controller's lever array

    // Reference to the GameObject representing the player or an empty GameObject following the player
    public GameObject playerHandleDoor;

    // Range within which the player can interact with the door
    private float interactionRange = 2f;

    // Flag to track if the door is open or closed
    private bool isOpen = false;

    // The initial position of the object
    private Vector3 initialPosition;

    // The position when the lever is activated
    private Vector3 activatedPosition;

    // Flag to track if the lever is currently activated
    private bool isLeverActivated = false;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
        // Calculate the activated position when the lever is activated
        activatedPosition = new Vector3(initialPosition.x - 0.28f, initialPosition.y - 0.2f, initialPosition.z);
    }

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
                leverAnimator.SetBool("Activated", isOpen);

                // Inform the door controller about the lever state change
                doorController.UpdateLeverActivation(leverIndex, leverAnimator.GetBool("Activated"));

                // Set the lever activation state
                isLeverActivated = isOpen;

                // Move the object based on the lever activation state
                if (isLeverActivated)
                {
                    // Move the object to the activated position immediately
                    transform.position = activatedPosition;
                }
                else
                {
                    // Move the object back to its initial position immediately
                    transform.position = initialPosition;
                }
            }
        }
    }
}
