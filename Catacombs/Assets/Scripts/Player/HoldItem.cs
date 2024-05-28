using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Transform holdPosition; // The position where the item will be held
    private GameObject heldItem; // Reference to the currently held item

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If the player is not currently holding an item
            if (heldItem == null)
            {
                // Find all colliders within a certain radius around the player's position
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

                // Iterate through all colliders found
                foreach (Collider2D collider in colliders)
                {
                    // Check if the collider belongs to an object tagged as "Pickupable"
                    if (collider.CompareTag("Pickupable"))
                    {
                        // Assign the heldItem to the found object
                        heldItem = collider.gameObject;
                        // Make the heldItem's Rigidbody2D kinematic to prevent physics interactions
                        heldItem.GetComponent<Rigidbody2D>().isKinematic = true;
                        // Set the parent of the heldItem to the holdPosition
                        heldItem.transform.parent = holdPosition;
                        // Reset the local position of the heldItem to (0, 0, 0) relative to the holdPosition
                        heldItem.transform.localPosition = Vector3.zero;
                        break;
                    }
                }
            }
            else
            {
                // Make the heldItem's Rigidbody2D non-kinematic to allow physics interactions
                heldItem.GetComponent<Rigidbody2D>().isKinematic = false;
                // Set the parent of the heldItem to null (remove parent)
                heldItem.transform.parent = null;
                // Reset the reference to heldItem to null, indicating the player is not holding anything
                heldItem = null;
            }
        }
    }
}
