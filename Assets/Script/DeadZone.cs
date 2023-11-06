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
        //If the player enter the trigger, respawn it
        if(other.tag == "Player")
        {
            CoreSystem.PlayerAttacked();
            other.transform.position = respawnPoint;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
