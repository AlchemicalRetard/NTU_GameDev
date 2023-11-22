using UnityEngine;

public class Rat_Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform pointA;
    public Transform pointB;
    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointB; // Start moving towards pointB
    }

    void Update()
    {
        MoveTowardsTarget();
        CheckDistanceAndSwitchTargets();
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
}
