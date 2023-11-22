/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    public GameObject healthBar;
   // public GameObject timer;
    public Rigidbody2D playerRigidbody; // Assign this in the inspector

    static private int playerHealth = 7;
    static private HealthBarController healthBarController;
   // static private GameTimer gameTime2r;
    static private CoreSystem instance; // For non-static reference to this script

    // Recoil and slow motion settings
    public float recoilForce = 2f;
    public float slowMotionScale = 0.5f;
    public float slowMotionDuration = 2f;

    void Awake()
    {
        instance = this; // Assign the static instance
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBarController = healthBar.GetComponent<HealthBarController>();
       // gameTime2r = timer.GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    *//*static public void AddTime(int timeToAdd)
    {
        gameTime2r.AddTime(timeToAdd);
    }*//*

    static public CoreSystem Instance
    {
        get
        {
            if (instance == null)
            {
                // Optionally, find the CoreSystem in the scene if it's not already set.
                instance = FindObjectOfType<CoreSystem>();
            }
            return instance;
        }
    }

    public void PlayerAttacked(Vector2 attackDirection)
    {
        playerHealth--;
        healthBarController.SetHeart(playerHealth);
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            // Handle player death, e.g., trigger a game over sequence
        }
        else
        {
            // Apply recoil force in the direction opposite of the attack
            StartCoroutine(RecoilAndSlowMotion(-attackDirection));
        }
    }

    private IEnumerator RecoilAndSlowMotion(Vector2 recoilDirection)
    {
        // Apply recoil force
        playerRigidbody.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);

        // Slow down time
        Time.timeScale = slowMotionScale;
        // Ensure the duration is applied in scaled time by dividing by Time.timeScale
        yield return new WaitForSecondsRealtime(slowMotionDuration * Time.timeScale);

        // Restore normal time scale
        Time.timeScale = 1f;
    }
}

*//*


using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    public GameObject healthBar;

    private static int playerHealth = 7;
    private static HealthBarController healthBarController;
    private static CoreSystem instance; // For non-static reference to this script

    void Awake()
    {
        instance = this; // Assign the static instance
    }

    void Start()
    {
        healthBarController = healthBar.GetComponent<HealthBarController>();
    }

    public static CoreSystem Instance
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

    public void PlayerAttacked()
    {
        playerHealth--;
        healthBarController.SetHeart(playerHealth);
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            // Handle player death, e.g., trigger a game over sequence
        }
    }
}


*/
using System.Collections;
using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    public GameObject healthBar;
    public Animator playerAnimator; // Reference to the Animator component on the player

    private static int playerHealth = 7;
    private static HealthBarController healthBarController;
    private static CoreSystem instance; // For non-static reference to this script

    void Awake()
    {
        instance = this; // Assign the static instance
    }

    void Start()
    {
        healthBarController = healthBar.GetComponent<HealthBarController>();
        // Ensure that the playerAnimator reference is set,
        // either via the inspector or by finding the component at runtime if not set.
        if (!playerAnimator)
        {
            Debug.LogError("Player Animator reference not set in CoreSystem");
        }
    }

    public static CoreSystem Instance
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

    public void PlayerAttacked()
    {
        playerHealth--;
        healthBarController.SetHeart(playerHealth);
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            // Handle player death, e.g., trigger a game over sequence
        }
        else
        {
            // Trigger the hurt animation
            if (playerAnimator)
            {
                playerAnimator.SetTrigger("IsHurt"); // Assuming "IsHurt" is a trigger
                StartCoroutine(ResetHurtStateAfterDelay(1f)); // 2 seconds for the hurt animation to play
            }
        }
    }

    private IEnumerator ResetHurtStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the duration of the hurt animation

        // Now reset the hurt state
        if (playerAnimator)
        {
            // Assuming "IsHurt" is a boolean. If it's a trigger, you don't need to reset it.
            playerAnimator.SetBool("IsHurt", false);
        }
    }
}

