using UnityEngine;

public class CollectibleFosod : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FoodCounter.Instance.AddFood(1);
            Destroy(gameObject);  // Destroy the food item
        }
    }
}
