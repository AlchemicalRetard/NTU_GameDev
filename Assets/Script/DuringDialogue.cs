using UnityEngine;

public class DuringDialogue : MonoBehaviour
{
    public MonoBehaviour playerMovementScript; // Assign your player movement script here
    public Rigidbody2D playerRigidbody2D; // Assign your player Rigidbody2D here
    public GameTime2r gameTimeScript; // Assign your GameTime2r script here

    // Call this method to pause player movement and timer
    public void PauseGameplay()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false; // Disable player movement
            playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
        if (gameTimeScript != null)
        {
            gameTimeScript.timerIsRunning = false; // Stop the timer
        }
    }

    // Call this method to resume player movement and timer
    public void ResumeGameplay()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true; // Enable player movement
            playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (gameTimeScript != null)
        {
            gameTimeScript.timerIsRunning = true; // Resume the timer
        }
    }
}
