using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour
{
    public void SearchDrone(string droneId)
    {
        Debug.Log($"Searching for drone with ID: {droneId}");
        StartCoroutine(SimulateSearch(droneId));
    }

    private IEnumerator SimulateSearch(string droneId)
    {
        yield return new WaitForSeconds(2.0f); // Simulate a delay
        Debug.Log($"Drone with ID: {droneId} found!");

        // Update the UI message
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateMessage($"Drone with ID: {droneId} found!");
        }
    }

    public void SelfDestructDrone(string droneId)
    {
        Debug.Log($"Self-destructing drone with ID: {droneId}");
        StartCoroutine(SimulateSelfDestruct(droneId));
    }

    private IEnumerator SimulateSelfDestruct(string droneId)
    {
        yield return new WaitForSeconds(2.0f); // Simulate a delay for self-destruct
        Debug.Log($"Drone with ID: {droneId} has self-destructed!");

        // Update the UI message
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateMessage($"Drone with ID: {droneId} has self-destructed!");
        }
    }
}