using UnityEngine;

public class Rat_Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    private Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform; // Start moving towards pointB
    }

    void Update()
    {
        MoveTowardsCurrentPoint();
        CheckDistanceToCurrentPoint();
    }

    private void MoveTowardsCurrentPoint()
    {
        // Determine the direction to move
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, step);
    }

    private void CheckDistanceToCurrentPoint()
    {
        // Check if the enemy is close enough to the target point to consider it "reached"
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f) // Using a smaller distance for accuracy
        {
            // Switch to the other point
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
        }
    }
}
