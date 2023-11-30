using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreSystem : MonoBehaviour
{
    public enum GameEndReason
    {
        Undefined = -1,
        PlayerDead = 0,
        TimeUp = 1,
        TutorialClear = 2
    }

    public GameObject healthBar;
    public GameObject timer;
    public GameObject levelLoaderObject;
    public Animator playerAnimator; // Reference to the Animator component on the player

    static private int playerHealth = 3;
    static private HealthBarController healthBarController;
    static private GameTime2r gameTime2r;
    static private GameEndReason gameEndReason = GameEndReason.Undefined;
    static private LevelLoader levelLoader;
    static public CoreSystem instance; // For non-static reference to this script

    void Awake()
    {
        instance = this; // Assign the static instance
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            healthBarController = healthBar.GetComponent<HealthBarController>();
        }
        catch
        {
            print("Health bar not found");
        }

        try
        {
            gameTime2r = timer.GetComponent<GameTime2r>();
        }
        catch
        {
            print("Timer not found");
        }

        try
        {
            levelLoader = levelLoaderObject.GetComponent<LevelLoader>();
        }
        catch
        {
            print("Level loader not found");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void addTime(int timeToAdd)
    {
        gameTime2r.addTime(timeToAdd);
    }

    // static public void PlayerAttacked(){
    //     if(playerIsHurtable()){
    //         playerHealth--;
    //     }
    //     healthBarController.SetHeart(playerHealth);
    //     print("Player Health: " + playerHealth);
    //     if(playerHealth <= 0){
    //         print("Player is dead");
    //         gameEndReason = GameEndReason.PlayerDead;
    //         LoadLevel("GameEndScene");
    //     }
    // }

    public void PlayerAttacked()
    {
        playerHealth--;
        healthBarController.SetHeart(playerHealth);
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            gameEndReason = GameEndReason.PlayerDead;
            LoadLevel("GameEndScene");
        }
        else
        {
            // Trigger the hurt animation
            if (playerAnimator)
            {
                playerAnimator.Play("Meow-Knight_Take_Damage");
                // playerAnimator.SetTrigger("IsHurt"); // Assuming "IsHurt" is a trigger
                // StartCoroutine(ResetHurtStateAfterDelay(1f)); // 2 seconds for the hurt animation to play
            }
        }
    }

    // private IEnumerator ResetHurtStateAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay); // Wait for the duration of the hurt animation

    //     // Now reset the hurt state
    //     if (playerAnimator)
    //     {
    //         // Assuming "IsHurt" is a boolean. If it's a trigger, you don't need to reset it.
    //         playerAnimator.SetBool("IsHurt", false);
    //     }
    // }

    static bool playerIsHurtable()
    {
        //Final door logic might not be in the scene, 
        //so we move this check in a seperate function lest the "PlayerAttacked" function looks strange
        return !FinalDoorLogic.isCameraMoving();
    }

    static public void setGameEndReason(GameEndReason reason)
    {
        gameEndReason = reason;
    }

    static public GameEndReason getGameEndReason()
    {
        return gameEndReason;
    }

    static public int getPlayerHealth()
    {
        return playerHealth;
    }

    static public void LoadLevel(string levelName)
    {
        levelLoader.LoadLevel(levelName);
    }
}