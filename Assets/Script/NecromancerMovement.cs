using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMovement : MonoBehaviour, IDamageable
{
    public float speed;
    public float jumpHeight;
    public float maxSpeed;
    public float detectRange;
    public float attackZone;
    public float attackInterval;
    public AudioClip[] attackSound;
    private AudioSource audioSource;
    public GameObject flameSpawnPoint;
    public GameObject flame;
    public float launchVelocity;

    private Rigidbody2D rb;
    private Animator animator;
    private float originalGravityScale;
    private LayerMask mask;

    private bool isGrounded = false;
    private bool attacked = false;
    private bool facingRight = true;

    public int health = 3; // Enemy health
    private float lastAttackTime = 0;
   
    

    void Awake()
    {
        mask = LayerMask.GetMask("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
        facingRight = transform.localScale.x > 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce health

        if (health <= 0)
        {
            attacked = true;
            Die(); // Call Die method when health is 0 or less
        }
        else
        {
            // Optionally, play a damage animation or sound
            attacked = true; // Keep or modify this line based on your logic
            animator.Play("Necromancer_GetHit");
        }
    }

    private void Die()
    {
        // Play death animation or sound
        StartCoroutine("Destroy"); // Destroy the enemy object
    }


    // Update is called once per frame
    void Update()
    {
        //Check if player is in sight
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.5f, 0), new Vector3(facingRight ? 1 : -1, 0, 0), detectRange, mask);
        Debug.DrawRay(transform.position - new Vector3(0, 0.5f, 0), new Vector3(facingRight ? 1 : -1, 0, 0) * detectRange, Color.green);

        //if player is in sight, move towards player
        float x = 0;
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            float distance = hit.collider.transform.position.x - transform.position.x;

            //Always try to face player when detected, however, player can just jump to avoid being detected
            Flip(distance);

            //Player is in sight, should we attack or just move towards player?
            if (Mathf.Abs(distance) < attackZone)
            {
                //if we are too close to player, first shift a little bit so we don't overlap too much, 
                //which will seem like we're attacking nothing
                // if (Mathf.Abs(distance) < attackZone * 0.8f)
                // {
                //     transform.position = new Vector3(transform.position.x + (facingRight ? -1 : 1) * (attackZone - Mathf.Abs(distance)), transform.position.y, transform.position.z);
                // }
                //attack if cooldown is over
                if (Time.time - lastAttackTime > attackInterval && !attacked)
                {
                    lastAttackTime = Time.time;
                    StartCoroutine("AttackPlayer", hit.collider.gameObject.transform.position);
                    //audioSource.PlayOneShot(attackSound[Random.Range(0, attackSound.Length)], 1f);
                }
            }
            else
            {
                //move towards player
                x = distance > 0 ? 1 : -1;
            }
        }

        //This part below is for movement
        animator.SetFloat("Speed", Mathf.Abs(x));

        //move horizontally
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(x * speed, 0f), ForceMode2D.Impulse);
            Flip(x);
        }

        //make it not slippery whem not intending to move
        if (Mathf.Abs(x) < 0.001f && isGrounded)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        //make falling faster
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = originalGravityScale * 6;
        }
        else
        {
            rb.gravityScale = originalGravityScale * 3;
        }

        //jump
        bool jump = false;
        if (jump && Mathf.Abs(rb.velocity.y) < 0.001f && isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.Play("Fox_Jump");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("_groundLayer"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("_groundLayer"))
        {
            isGrounded = false;
        }
    }

    void Flip(float x)
    {
        if (x > 0 && !facingRight || x < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    IEnumerator AttackPlayer(Vector3 playerPosition)
    {
        // yield return new WaitForSeconds(.f);
        animator.Play("Necromancer_Attack");
        // spawn flame and fly toward player
        Vector2 flyDirection = (playerPosition - flameSpawnPoint.transform.position).normalized * launchVelocity;
        GameObject tempFlame = Instantiate(flame, flameSpawnPoint.transform.position, Quaternion.identity);
        tempFlame.GetComponent<Rigidbody2D>().velocity = flyDirection;
        return null;
    }

    IEnumerator Destroy()
    {
        animator.Play("Necromancer_GetHit");
        yield return new WaitForSeconds(0.25f);
        animator.Play("Necromancer_Death");
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}