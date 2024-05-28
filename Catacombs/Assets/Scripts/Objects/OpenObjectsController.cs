using Unity.VisualScripting;
using UnityEngine;

public class OpenObjectsController : MonoBehaviour
{
    // Reference to the door animator
    public Animator objectAnimator;

    // Reference to the GameObject representing the player or an empty GameObject following the player
    public GameObject playerEmpty;

    // Range within which the player can interact with the door
    private float interactionRange = 2f;

    public string requiredField;

    // Reference to the TilemapDestroy script
    public TilemapDestroy tilemapDestroy;
    public bool selectTile;

    // Flag to track if the door is open or closed
    private bool condition = false;

    private bool openedChest = false;

    void Update()
    {
        // Check if the distance between the door and the player is within the interaction range
        if (Vector2.Distance(transform.position, playerEmpty.transform.position) <= interactionRange)
        {
            // Check if the player presses the interaction key (e.g., "E")
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Toggle the door state
                condition = !condition;

                // Set the "OpenDoor" parameter in the door animator based on the door state
                objectAnimator.SetBool(requiredField, condition);

                // If the condition is "ActiveChest", move the object along the y-axis
                if (condition && requiredField == "ActiveChest" && !openedChest)
                {
                    // Move the object by 0.2 along the y-axis
                    transform.position += new Vector3(0f, 0.18f, 0f);

                    openedChest = true;
                }

                if (selectTile && tilemapDestroy != null)
                {
                    tilemapDestroy.DestroyTilemapAndCollider();
                }
            }
        }
    }
}
