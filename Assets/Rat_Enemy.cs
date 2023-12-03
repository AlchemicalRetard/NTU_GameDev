using UnityEngine;

public class Rat_Enemy : MonoBehaviour, IDamageable
{
    public int health = 3; // Enemy health
    public float moveSpeed = 2f;

    public Transform pointA;
    public Transform pointB;

    private Transform targetPoint;
    private Vector3 lastPosition;

    
    void Start()
    {
        targetPoint = pointB; // Start moving towards pointB
        lastPosition = transform.position;
    }

    void Update()
    {
        MoveTowardsTarget();
        CheckDistanceAndSwitchTargets();
        FlipSpriteBasedOnDirection();
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce health

        if (health <= 0)
        {
            Die(); // Call Die method when health is 0 or less
        }
        else
        {
            // Optionally, play a damage animation or sound
        }
    }




    void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
    }

    void CheckDistanceAndSwitchTargets()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }

    void FlipSpriteBasedOnDirection()
    {
        if (transform.position.x > lastPosition.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x < lastPosition.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        lastPosition = transform.position; // Update the last position for the next frame
    }

    private void Die()
    {
        // Play death animation or sound
        Destroy(gameObject); // Destroy the enemy object
    }

}
