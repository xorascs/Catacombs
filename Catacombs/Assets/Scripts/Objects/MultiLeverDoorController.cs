using UnityEngine;
using System.Collections.Generic;

public class MultiLeverDoorController : MonoBehaviour
{
    // Reference to the door animator
    public Animator doorAnimator;
    public GameObject[] levers;

    // Flag to track if each lever is currently activated
    private bool[] isLeverActivated;

    // List of lever indices required to open the door
    public List<int> requiredLeverIndexies = new List<int>();

    public bool randomLeverIndexies = false;

    void Start()
    {
        // Initialize the lever activation state array
        isLeverActivated = new bool[levers.Length];

        // Generate random required lever indices
        if (randomLeverIndexies) { GenerateRandomRequiredLeverIndices(); }
        
    }

    void Update()
    {
        // Check if all required levers are activated
        bool allRequiredLeversActivated = CheckRequiredLeversActivated();

        // Set the "OpenDoor" parameter in the door animator based on the door state
        doorAnimator.SetBool("OpenDoor", allRequiredLeversActivated);
    }

    // Function to generate random required lever indices
    private void GenerateRandomRequiredLeverIndices()
    {
        requiredLeverIndexies.Clear();
        int numRequiredLevers = Random.Range(1, levers.Length + 1); // Random number of required levers
        List<int> availableIndices = new List<int>(levers.Length);
        for (int i = 0; i < levers.Length; i++)
        {
            availableIndices.Add(i);
        }

        // Shuffle the available indices list
        for (int i = 0; i < availableIndices.Count; i++)
        {
            int temp = availableIndices[i];
            int randomIndex = Random.Range(i, availableIndices.Count);
            availableIndices[i] = availableIndices[randomIndex];
            availableIndices[randomIndex] = temp;
        }

        // Take the required number of indices from the beginning of the shuffled list
        for (int i = 0; i < numRequiredLevers; i++)
        {
            requiredLeverIndexies.Add(availableIndices[i]);
        }
    }


    // Function to check if all required levers are activated
    private bool CheckRequiredLeversActivated()
    {
        foreach (int leverIndex in requiredLeverIndexies)
        {
            if (!isLeverActivated[leverIndex])
            {
                return false;
            }
        }

        // Check if any non-required lever is activated
        for (int i = 0; i < isLeverActivated.Length; i++)
        {
            if (!requiredLeverIndexies.Contains(i) && isLeverActivated[i])
            {
                return false; // Door should not open if any non-required lever is activated
            }
        }

        return true;
    }

    // Function to update the activation state of a lever at a given index
    public void UpdateLeverActivation(int leverIndex, bool activationState)
    {
        isLeverActivated[leverIndex] = activationState;
    }
}
