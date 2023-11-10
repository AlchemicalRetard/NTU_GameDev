using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Vector3 respawnPoint;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the player enter the trigger, respawn it
        if(other.tag == "Player")
        {
            audioSource.time = 0.52f;
            audioSource.Play();
            CoreSystem.PlayerAttacked();
            //if player is already dead, don't teleport it
            if(CoreSystem.getPlayerHealth() > 0){
                other.transform.position = respawnPoint;
            }
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
