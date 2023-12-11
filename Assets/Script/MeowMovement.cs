using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowMovement : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float maxSpeed;
    public AudioClip attackSound;
    public AudioClip meowAttackSound;

    public GameObject dashVFXPrefab; // Assign this in the Inspector
    public Transform dashVFXPosition;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private float originalGravityScale;
    private bool isGrounded = false;
    private bool isOnEnemy = false;
    private bool isAlive = true;
    [HideInInspector] public bool isAttacking = false;
    private LayerMask mask;

    private float damageCooldown = 1.0f; // 1 second cooldown
    private float lastDamageTime;

    public float dashSpeed = 10f;
    private float lastDashTime;
    private float dashCooldown = 1.0f;
    


    void Awake()
    {
        mask = LayerMask.GetMask("Enemy");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
        facingRight = transform.localScale.x > 0;
        audioSource = GetComponent<AudioSource>();
        lastDashTime = -dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.O))
        {
            if (Time.time >= lastDashTime + dashCooldown)
            {
                Dash();
                lastDashTime = Time.time;
            }
        }

        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(x));

        //move horizontally
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(x * speed, 0f), ForceMode2D.Impulse);
            Flip(x);
        }

        //make is not slippery whem not intending to move
        if(Mathf.Abs(x) < 0.75f && isGrounded)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        //make falling faster
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = originalGravityScale * 3;
        }
        else
        {
            rb.gravityScale = originalGravityScale * 3;
        }

        //jump
        bool jump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if(jump && (Mathf.Abs(rb.velocity.y) < 0.001f || isOnEnemy) && isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * originalGravityScale * 3));
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.Play("Meow-Knight_Jump");
        }

        //attack
        bool attack = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.P);
        if(attack && !isAttacking)
        {
            StartCoroutine(InAttackMode());
            animator.Play("Meow-Knight_Attack2");
            audioSource.PlayOneShot(attackSound, 1f);
            if(Random.Range(0, 5) == 0)
            {
                audioSource.PlayOneShot(meowAttackSound, 1f);
            }
        }

        Debug.DrawRay(transform.position, new Vector3(facingRight ? 1 : -1, 0, 0) * 1.5f, Color.red);
    }

    private void Dash()
    {
        float direction = facingRight ? 1f : -1f;
        rb.AddForce(new Vector2(direction * dashSpeed, 0), ForceMode2D.Impulse);

        // Instantiate dash VFX
        if (dashVFXPrefab != null && dashVFXPosition != null)
        {
            GameObject dashVFX = Instantiate(dashVFXPrefab, dashVFXPosition.position, Quaternion.identity);
            Destroy(dashVFX, 1.0f); // Destroy the VFX after 1 second
        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("_groundLayer"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isGrounded = false;
            isOnEnemy = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("_groundLayer"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isGrounded = true;
            isOnEnemy = false;

            // Player has collided with enemy, isn't attacking, and cooldown has passed
            // if (!isAttacking && Time.time > lastDamageTime + damageCooldown)
            // {
            //     lastDamageTime = Time.time; // Update the last damage time
            //     CoreSystem.instance.PlayerAttacked();
            //     ApplyRecoil(collision.transform.position);   
            // }
        }
    }

    void ApplyRecoil(Vector3 enemyPosition)
    {
        Vector2 attackDirection = (transform.position - enemyPosition).normalized;
        float recoilForce = 5.0f; // Adjust this force as necessary
        rb.velocity = Vector2.zero; // Reset velocity before applying recoil to make it consistent
        rb.AddForce(attackDirection * recoilForce, ForceMode2D.Impulse);
       
    }

    /*void Flip(float x)
    {
        if (x > 0 && !facingRight || x < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }*/
    // New flip for cinemachine camera offset
    void Flip(float x)
    {
        bool shouldFlip = (x > 0 && !facingRight) || (x < 0 && facingRight);
        if (shouldFlip)
        {
            // Flip the character by rotating 180 degrees around the Y axis
            Vector3 rotator = transform.eulerAngles;
            rotator.y += 180f;
            transform.eulerAngles = rotator;

            // Toggle the state of facingRight
            facingRight = !facingRight;
        }
    }

    IEnumerator InAttackMode()
    {
        isAttacking = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.5f, 0), new Vector3(facingRight ? 1 : -1, 0, 0), 1.5f, mask);
        if (hit.collider != null && (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Necromancer"))) {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(15);
            }
        }

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    public void Die()
    {
        isAlive = false;
    }
}
