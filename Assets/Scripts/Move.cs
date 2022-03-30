using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    // Variables de velocidad y fuerza de salto.
    public float speed = 10f;
    public float jumpForce = 15f;

    // Instancia de elementos de Unity.
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;

    // Variables de salto y checks relacionados.
    public int extraJump = 1;
    int jumpCount = 0;
    bool isGrounded;
    float jumpCoolDown;

    // Variables de movimiento y perspectiva.
    float moveInput;
    private Vector2 movement;
    private bool facingRight = true;

    // Variables del dash y checks relacionados.
    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    // Variables para la animación del sprite.
    [Header("Animation")]
    private Animator animator;

    // Función de inicio.
    void Start()
    {
        //Instancia del componente Animation.
        animator = GetComponent<Animator>();
    }

    // Función que se actualiza constantemente.
    private void Update()
    {
        // Movimiento horizontal del personaje.
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", Mathf.Abs(moveInput));
        movement = new Vector2(moveInput, 0f);

        // Perspectiva del personaje según donde mire.
        if (moveInput < 0f && facingRight == true)
        {
            Flip();
        }
        else if (moveInput > 0f && facingRight == false)
        {
            Flip();
        }

        // Boton de salto para ejecutar la acción.
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Botones de dash para ejecutar la acción.
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

    // Función de actualización arreglada.
    private void FixedUpdate()
    {
        // Si esta dasheando el personaje, ejecutar el dash.
        if (!isDashing)
        {
            float horizontalVelocity = movement.normalized.x * speed;
            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        }
    }

    // Función de perspectiva del sprite (eje X).
    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Función de salto doble.
    void Jump()
    {
        // Si hay saltos o se toco el suelo, se ejecutan los saltos según el limite establecido (1 salto extra).
        if (isGrounded || jumpCount < extraJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    // Función de chequeo al tocar el suelo.
    void CheckGrounded()
    {
        // Si se toca el suelo o sigue habiendo saltos disponibles, se puede seguir saltando.
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

    // Coorutina para el dash del personaje.
    IEnumerator Dash(float direction)
    {
        // Realiza el dashing como true si se invoco la coorutina, para ejecutar el 
        // movimiento bajo el eje X en una gravedad 0 para que el dash sea adecuado,
        // después de un tiempo cambia de estado y la gravedad se restablece.
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = 5f;
    }
}


