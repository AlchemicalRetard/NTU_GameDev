using UnityEngine;

public class FallingPrefabSpawner : MonoBehaviour
{
    public GameObject prefabToFall; // The prefab to instantiate
    public Transform startPosition; // Start position for the range
    public Transform endPosition;   // End position for the range
    public float spawnInterval = 1f; // Time interval between spawns

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFallingPrefab();
            timer = 0f;
        }
    }

    void SpawnFallingPrefab()
    {
        if (prefabToFall != null && startPosition != null && endPosition != null)
        {
            // Generate a random position between startPosition and endPosition
            float randomX = Random.Range(startPosition.position.x, endPosition.position.x);
            Vector3 spawnPosition = new Vector3(randomX, startPosition.position.y, startPosition.position.z);

            // Instantiate the prefab at the generated position
            GameObject fallingObject = Instantiate(prefabToFall, spawnPosition, Quaternion.identity);

            // Add a Rigidbody component to the instantiated prefab to make it fall
            Rigidbody rb = fallingObject.AddComponent<Rigidbody>();

            // Optionally, adjust Rigidbody properties here (e.g., mass, drag)
            // rb.mass = 1; 
            // rb.drag = 0;

            // Destroy the object after a certain time (e.g., 2 seconds)
            Destroy(fallingObject, 2f);
        }
    }

}
