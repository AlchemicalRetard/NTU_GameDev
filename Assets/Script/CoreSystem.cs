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
        TutorialClear = 2,
        GameClear = 3
    }

    public enum Language
    {
        English = 0,
        Chinese = 1
    }

    public GameObject healthBar;
    public GameObject timer;
    public GameObject levelLoaderObject;
    public MeowMovement playerScript; // Reference to the player object
    public Animator playerAnimator; // Reference to the Animator component on the player

    static private int playerHealth = 3;
    static private HealthBarController healthBarController;
    static private GameTime2r gameTime2r;
    static private GameEndReason gameEndReason = GameEndReason.Undefined;
    static private string lastSelectedLevelName = "";
    static private LevelLoader levelLoader;
    static private Language language = Language.Chinese;
    static public CoreSystem instance; // For non-static reference to this script
    static public string clearTime = "00:00";
    static public int heartLeft = 0;

    void Awake()
    {
        instance = this; // Assign the static instance
        playerHealth = 3;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 3;
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

    public static CoreSystem Instance // Property for instance access
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CoreSystem>();
            }
            return instance;
        }
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
            StartCoroutine(PlayerDie());
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

    IEnumerator PlayerDie()
    {
        playerScript.Die();
        //playerAnimator.Play("Meow-Knight_Death");
        gameEndReason = GameEndReason.PlayerDead;
        yield return new WaitForSeconds(0.5f);
        LoadLevel("GameEndScene");
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
        clearTime = gameTime2r.getClearTime();
        heartLeft = playerHealth;
        levelLoader.LoadLevel(levelName);
    }

    static public void changeLanguage()
    {
        language = (Language)(((int)language + 1) % 2);
    }

    static public Language getLanguage()
    {
        return language;
    }

    //This is only for title screen to remember the last selected level
    static public void setLastSelectedLevelName(string levelName)
    {
        lastSelectedLevelName = levelName;
    }

    static public string getLastSelectedLevelName()
    {
        return lastSelectedLevelName;
    }
}