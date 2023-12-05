using System.Collections;
using UnityEngine;

public class Rat_Enemy : MonoBehaviour, IDamageable
{
    public int health = 3; // Enemy health
    public float moveSpeed = 2f;
    public float attackZone = 1.0f;
    public float attackInterval = 2.0f;

    public Transform pointA;
    public Transform pointB;
   // public Transform rayCastOrigin;

    private Animator ratAnim;
    private Transform targetPoint;
    private Vector3 lastPosition;
    private float lastAttackTime; // Time when the last attack happened
    private LayerMask mask;
    private bool toDestroy = false;

    void Awake()
    {
        mask = LayerMask.GetMask("Player");
    }

    void Start()
    {
        lastAttackTime = -attackInterval;
        ratAnim = GetComponent<Animator>();
        targetPoint = pointB;
        lastPosition = transform.position;
    }

    void Update()
    {
        if(!toDestroy){
            MoveTowardsTarget();
            CheckDistanceAndSwitchTargets();
            FlipSpriteBasedOnDirection();
            PerformAttackRaycast();
        }
    }

    private void PerformAttackRaycast()
    {
        Vector2 rayDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.2f, 0), rayDirection, attackZone, mask);
        Debug.DrawRay(transform.position, rayDirection * attackZone, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (Time.time - lastAttackTime > attackInterval)
        {
            lastAttackTime = Time.time;
            ratAnim.Play("Rat_Attack_Anim");
            CoreSystem.instance.PlayerAttacked();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        toDestroy = true;
        moveSpeed = 0f;
        ratAnim.Play("Rat_Death_Anim");
        StartCoroutine(DestroyAfterDelay(1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
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
        lastPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Collision logic with player
        }
    }
}
