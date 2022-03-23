using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    private float moveInput; //get horizontal movement
    public float moveSpeed;
    public float jumpForce;
 
    public Transform groundCheck; //object to check whether player is touching the ground
    public LayerMask ground;
    public float groundCheckRadius;
    private bool isGrounded;
 
    public int playerJumps;
    private int tempPlayerJumps;

    public float dashDistance = 5f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode; 

 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDashing)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
            
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }
 
    private void Update()
    {
        if(isGrounded)
        {
            tempPlayerJumps = playerJumps;
        }
 
        if(Input.GetKeyDown(KeyCode.Space) && tempPlayerJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            tempPlayerJumps--;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
            }
            else 
            {
                doubleTapTime = Time.time + 0.5f;
            }

            lastKeyCode = KeyCode.A;
        }
    }

    IEnumerator Dash (float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(1f);
        isDashing = false;
        rb.gravityScale = gravity;
    }
}
