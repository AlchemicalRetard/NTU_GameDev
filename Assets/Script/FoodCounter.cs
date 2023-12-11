using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodCounter : MonoBehaviour
{
    public static FoodCounter Instance;
    public TextMeshProUGUI foodCountText;
    public AudioClip collectSound;
    public GameObject[] foodsIcon;

    private int totalFood = 0;
    private int collectedFood = 0;  // Track the number of collected food items
    private AudioSource audioSource;

    private void Awake()
    {


        if (Instance == null)
        {
            Instance = this;
        }
       /* else if (Instance != this)
        {
            Destroy(gameObject);
        }*/

       // DontDestroyOnLoad(gameObject);

        // Count the total number of food items in the scene
        totalFood = GameObject.FindGameObjectsWithTag("Food").Length;  // Assuming the food items are tagged as "Food"
        foodCountText.text = " x " + collectedFood + " / " + totalFood;

        // change all food icon to be transparent
        foreach (GameObject foodIcon in foodsIcon)
        {
            foodIcon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
        }
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

        // change the food icon to be opaque
        for(int i = 0; i < collectedFood; i++)
        {
            foodsIcon[i].GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
        }

        UpdateFoodCountText();
    }

    private void UpdateFoodCountText()
    {
        if (foodCountText != null)
        {
            foodCountText.text = " x " + collectedFood + " / " + totalFood ;
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
