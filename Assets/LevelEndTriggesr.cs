using UnityEngine;
using TMPro;

public class LevelEndTriggesr : MonoBehaviour
{
    public TextMeshProUGUI statusText;  // Reference to TextMeshProUGUI to display "Level Cleared" or "Game Over"
    public GameTime2r timer;  // Reference to the Timer script to check if time is up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && FoodCounter.Instance != null && timer != null)
        {
            if (timer.IsTimeUp())  // The method to check if time is up - add this to your Timer script
            {
                if (FoodCounter.Instance.AllFoodCollected())
                {
                    statusText.text = "Level Cleared!";
                }
                else
                {
                    statusText.text = "Game Over!";
                }
            }
           if (!timer.IsTimeUp()&& FoodCounter.Instance.AllFoodCollected())
            {
                statusText.text = "Level Cleared!";
            }
        }
    }
}
