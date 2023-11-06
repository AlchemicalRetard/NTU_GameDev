using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDrop : MonoBehaviour
{
    public GameObject[] food;
    public int foodItemsForNextStage;

    private int currentFoodCount;

    void Start()
    {
        StartCoroutine(DropFood());
    }

    IEnumerator DropFood()
    {
        while (currentFoodCount < foodItemsForNextStage)
        {
            // Instantiate a random food item at a random position
            GameObject foodItem = Instantiate(food[Random.Range(0, food.Length)], new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);

            // Increment the current food count
            currentFoodCount++;

            // Print to the console
            Debug.Log("Food dropped, current count: " + currentFoodCount);

            // Wait for 5 seconds before the next iteration
            yield return new WaitForSeconds(5f);
        }

        Debug.Log("Food drop complete.");
    }
}
