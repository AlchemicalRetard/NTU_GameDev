using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    static private int playerHealth = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void PlayerAttacked(){
        playerHealth--;
        print("Player Health: " + playerHealth);
        if(playerHealth <= 0){
            print("Player is dead");
        }
    }
}
