using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDestroy : MonoBehaviour
{
    // Reference to the Tilemap you want to destroy
    public Tilemap tilemap;

    // Reference to the BoxCollider2D attached to the GameObject
    public BoxCollider2D boxCollider;

    // Call this method to destroy the Tilemap and BoxCollider2D
    public void DestroyTilemapAndCollider()
    {
        // Clear all tiles from the Tilemap
        tilemap.ClearAllTiles();
        // Destroy the BoxCollider2D
        if (boxCollider != null)
        {
            Destroy(boxCollider);
        }
    }
}
