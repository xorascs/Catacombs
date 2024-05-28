using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this line to access UI components

public class DeathRestart : MonoBehaviour
{
    // Tag of the object that triggers level restart
    private string restartTag = "RestartObject";

    // You might want to add a delay before restarting the level
    public float restartDelay = .3f;
    private bool collisionDetected = false;

    // UI Text to display the "You are dead" message
    public Text deathText;

    private void Start()
    {
        // Disable the death text at the start
        deathText.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(restartTag) && !collisionDetected)
        {
            collisionDetected = true;
            // Show the "You are dead" text
            deathText.gameObject.SetActive(true);

            // Call the RestartLevel function after the restart delay
            Invoke("RestartLevel", restartDelay);
        }
    }

    // Restart the level
    void RestartLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
