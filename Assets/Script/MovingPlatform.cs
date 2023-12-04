using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;

    public Transform[] points; //IMPORTANT!! At least 2 points required
    private int targetPointIndex = 1;

    private Transform targetPoint;
    private Vector3 lastPosition;

    private GameObject target = null;
    private Vector3 offset;

    
    void Start()
    {
        targetPoint = points[targetPointIndex]; // Start moving towards pointB
        lastPosition = transform.position;
        
        target = null;
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
            targetPointIndex = (targetPointIndex + 1) % points.Length;
            targetPoint = points[targetPointIndex];
        }
    }

    void OnTriggerStay2D(Collider2D col){
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }

    void OnTriggerExit2D(Collider2D col){
        target = null;
    }

    void LateUpdate(){
        if (target != null) {
            target.transform.position = transform.position+offset;
        }
    }
}
