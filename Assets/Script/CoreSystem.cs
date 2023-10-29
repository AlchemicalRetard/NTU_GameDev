using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    public GameObject healthBar;

    static private int playerHealth = 3;
    static private HealthBarController healthBarController;

    // Start is called before the first frame update
    void Start()
    {
        healthBarController = healthBar.GetComponent<HealthBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void PlayerAttacked(){
        playerHealth--;
        healthBarController.SetHeart(playerHealth);
        print("Player Health: " + playerHealth);
        if(playerHealth <= 0){
            print("Player is dead");
        }
    }
}
