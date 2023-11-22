/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    [SerializeField] float health = 20f;

    void Start()
    {
        
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        
    }
}
*/

using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    public float health = 10f; // Starting health of the enemy.

    void Start()
    {
        // Any initialization for your SmallEnemy can go here.
    }

    // Call this method to damage the enemy.
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle the enemy death logic here.
        Destroy(gameObject); // This will destroy the enemy object.
    }
}
