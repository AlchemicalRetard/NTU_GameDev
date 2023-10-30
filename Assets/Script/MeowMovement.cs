using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowMovement : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float maxSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private float originalGravityScale;
    private bool isGrounded = false;
    private bool isAttacking = false;
    private LayerMask mask;

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
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(x));

        //move horizontally
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(x * speed, 0f), ForceMode2D.Impulse);
            Flip(x);
        }

        //make is not slippery whem not intending to move
        if(Mathf.Abs(x) < 0.001f && isGrounded)
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
        bool jump = Input.GetKeyDown(KeyCode.Space);
        if(jump && Mathf.Abs(rb.velocity.y) < 0.001f && isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.Play("Meow-Knight_Jump");
        }

        //attack
        bool attack = Input.GetKeyDown(KeyCode.E);
        if(attack && !isAttacking)
        {
            StartCoroutine(InAttackMode());
            animator.Play("Meow-Knight_Attack2");
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

    IEnumerator InAttackMode()
    {
        isAttacking = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(facingRight ? 1 : -1, 0, 0), 1.5f, mask);
        if(hit.collider != null && hit.collider.tag == "Enemy"){
            hit.collider.gameObject.GetComponent<FoxMovement>().Attacked();
        }
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }
}
