using UnityEngine;
using TMPro;

public class FoodCounter : MonoBehaviour
{
    public static FoodCounter Instance;

    private int totalFood = 0;
    private int collectedFood = 0;  // Track the number of collected food items
    public TextMeshProUGUI foodCountText;

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

    public bool AllFoodCollected()
    {
        return collectedFood >= totalFood;
    }
    public void AddFood(int amount)
    {
        collectedFood += amount;
        Debug.Log("Food collected: " + collectedFood);

        UpdateFoodCountText();
    }

    private void UpdateFoodCountText()
    {
        if (foodCountText != null)
        {
            foodCountText.text = "Food Collected: " + collectedFood + " / " + totalFood;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned in FoodCounter.");
        }
    }
}
