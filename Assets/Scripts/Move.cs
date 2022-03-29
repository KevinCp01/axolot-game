using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 15f;
    public int extraJump = 1;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;

    int jumpCount = 0;
    bool isGrounded;
    float jumpCoolDown;

    float moveInput;
    private Vector2 movement;
    private bool facingRight = true;

    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if(isGrounded == true && isDashing == false)
        {
            animator.SetBool("isJumping", false);
            animator.SetFloat("Horizontal", Mathf.Abs(moveInput));
        }
       
        movement = new Vector2(moveInput, 0f);

        if (moveInput < 0f && facingRight == true)
        {
            Flip();
        }
        else if (moveInput > 0f && facingRight == false)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump"))
        {
        
            Jump();
        }
        

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.LeftArrow)
            {
                StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }

            lastKeyCode = KeyCode.LeftArrow;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.RightArrow)
            {
                StartCoroutine(Dash(1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }

            lastKeyCode = KeyCode.RightArrow;
        }

        CheckGrounded();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            float horizontalVelocity = movement.normalized.x * speed;
            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        if (isGrounded || jumpCount < extraJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
           
        }
    }

    void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer))
        {
            isGrounded = true;
            
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;
            
        }
        else if (Time.time < jumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = 5f;
    }
}
