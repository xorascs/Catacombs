using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Vector2 detectionSize = new Vector2(2.0f, 2.0f);
    [SerializeField] private string targetLevelName;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, detectionSize, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Load the target level
                LoadTargetLevel();
                break; // Exit the loop after loading the level
            }
        }
    }

    private void LoadTargetLevel()
    {
        SceneManager.LoadScene(targetLevelName);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectionSize);
    }
}
