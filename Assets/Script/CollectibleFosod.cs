using System.Collections;
using UnityEngine;

public class CollectibleFosod : MonoBehaviour
{
    public int timeToAdd = 10;

    void Start()
    {
        //Start floating animation coroutine
        StartCoroutine("FloatingAnimation");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          //  FoodCounter.Instance.AddFood(1);
          //  CoreSystem.addTime(timeToAdd);
            Destroy(gameObject);  // Destroy the food item
        }
    }

    IEnumerator FloatingAnimation()
    {
        float originalY = transform.position.y;
        while (true)
        {
            float newY = originalY + 0.1f * Mathf.Sin(Time.time * 1.5f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }
}
