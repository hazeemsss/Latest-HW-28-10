using UnityEngine;
using UnityEngine.UI; // Required for UI components
using TMPro; // Required for TextMeshPro components

public class UIManager : MonoBehaviour
{
    // Public fields for UI components
    public TMP_InputField droneIdInput; // Input field for drone ID
    public TMP_Text messageText; // Text for displaying messages (TextMeshPro)
    public TMP_Text fpsText; // Text for displaying FPS (TextMeshPro)
    public Button searchButton; // Button for searching drones
    public Button selfDestructButton; // Button for self-destructing drones

    private DroneController droneController; // Reference to the DroneController

    private void Start()
    {
        droneController = FindObjectOfType<DroneController>(); // Find the DroneController in the scene

        // Add listeners to buttons
        searchButton.onClick.AddListener(SearchDrone);
        selfDestructButton.onClick.AddListener(SelfDestructDrone);
    }

    private void Update()
    {
        // Update FPS display
        float fps = 1.0f / Time.deltaTime;
        fpsText.text = $"FPS: {fps:0}";
    }

public void SearchDrone()
{
    string droneId = droneIdInput.text.Trim(); // Get the drone ID from the input field and trim whitespace
    if (string.IsNullOrEmpty(droneId) || droneId == "Enter Drone ID:") // Check for empty input or placeholder
    {
        messageText.text = "Please enter a valid Drone ID.";
        return;
    }

    Debug.Log($"Search button clicked with Drone ID: {droneId}");
    messageText.text = "Searching for drone...";
    droneController.SearchDrone(droneId); // Call the method in DroneController
}

    public void SelfDestructDrone()
{
    string droneId = droneIdInput.text; // Get the drone ID from the input field
    if (string.IsNullOrEmpty(droneId))
    {
        messageText.text = "Please enter a valid Drone ID.";
        return;
    }

    Debug.Log($"Self-destruct button clicked with Drone ID: {droneId}");
    messageText.text = "Self-destructing drone...";
    droneController.SelfDestructDrone(droneId); // Call the method in DroneController
}

    public void UpdateMessage(string message)
{
    messageText.text = message; // Update the message text
}
}