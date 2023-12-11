using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera objectiveCamera;
    public Transform objectiveLocation;
    public float transitionDelay = 5f; // Time before switching back

    private void Awake()
    {
        // Ensure the main camera is active initially
        mainCamera.Priority = 11;
        objectiveCamera.Priority = 9;
    }

    public void ShowObjective()
    {
        objectiveCamera.Priority = 11; // Higher than main camera
        objectiveCamera.LookAt = objectiveLocation;
        StartCoroutine(ReturnToMainCameraAfterDelay(transitionDelay));
    }

    private IEnumerator ReturnToMainCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectiveCamera.Priority = 9; // Lower than main camera
    }
}
