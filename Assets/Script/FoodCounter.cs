using UnityEngine;
using TMPro;

public class FoodCounter : MonoBehaviour
{
    public static FoodCounter Instance;
    public TextMeshProUGUI foodCountText;
    public AudioClip collectSound;

    private int totalFood = 0;
    private int collectedFood = 0;  // Track the number of collected food items
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Count the total number of food items in the scene
        totalFood = GameObject.FindGameObjectsWithTag("Food").Length;  // Assuming the food items are tagged as "Food"
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool AllFoodCollected()
    {
        return collectedFood >= totalFood;
    }

    public void AddFood(int amount)
    {
        collectedFood += amount;
        audioSource.PlayOneShot(collectSound);
        Debug.Log("Food : " + collectedFood);

        UpdateFoodCountText();
    }

    private void UpdateFoodCountText()
    {
        if (foodCountText != null)
        {
            foodCountText.text = " x " + collectedFood;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned in FoodCounter.");
        }
    }

    public int getFoodCount(){
        return collectedFood;
    }
}
