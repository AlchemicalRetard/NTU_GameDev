using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the trigger, respawn it
        if (other.CompareTag("Player"))
        {
            // Assuming PlayerAttacked method is part of CoreSystem and requires a direction vector,
            // you may want to pass a default value if there's no specific attack direction in this context
            Vector2 defaultAttackDirection = Vector2.zero; // Replace with appropriate value if needed

            // Use the instance of CoreSystem to call the PlayerAttacked method
            //  CoreSystem.Instance.PlayerAttacked(defaultAttackDirection);

            // Move the player to the respawn point
            StartCoroutine(respawn());
        }
        else
        {
            // Destroy other objects that enter the DeadZone
            Destroy(other.gameObject);
        }

        IEnumerator respawn()
        {
            yield return new WaitForSeconds(0f); // created this to check if i can delay the respawn but this looks good :>
            other.transform.position = respawnPoint;
            
        }
    }

}
