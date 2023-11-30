using UnityEngine;

public class Necromancer_Enemy : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;

    private Animator animator;
    private Vector3 lastPosition;
    private float progress = 0.0f; // Progress between point A and B

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        progress += speed * Time.deltaTime;
        Vector3 newPosition = Vector3.Lerp(pointA, pointB, Mathf.PingPong(progress, 1));
        transform.position = newPosition;

        // Flip the sprite based on direction
        if (newPosition.x > lastPosition.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (newPosition.x < lastPosition.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        lastPosition = transform.position; // Update the last position for the next frame
    }
}
