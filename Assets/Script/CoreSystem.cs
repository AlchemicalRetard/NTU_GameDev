using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject timer;

    static private int playerHealth = 3;
    static private HealthBarController healthBarController;
    static private GameTime2r gameTime2r;

    // Start is called before the first frame update
    void Start()
    {
        healthBarController = healthBar.GetComponent<HealthBarController>();
        gameTime2r = timer.GetComponent<GameTime2r>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void addTime(int timeToAdd)
    {
        gameTime2r.addTime(timeToAdd);
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
