using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    
    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    private float defaultJumpForce;

    private bool canDoubleJump;


    private bool playerUnlocked;


    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        defaultJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorControllers();

        if (playerUnlocked)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        CheckCollision();
        CheckInput();

    }

    private void AnimatorControllers()
    {
        anim.SetBool("canDoubleJump", canDoubleJump);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        
    }

    private void CheckCollision()
    {
        // Raycast -> starting position, direction, distance, mask layer
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Fire2"))
            playerUnlocked = true;


        if (Input.GetButtonDown("Jump"))
            JumpButton();
    }

    private void JumpButton()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (canDoubleJump)
        {
            jumpForce = doubleJumpForce;
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // changse jumpForce back to default after we do the double jump;
            jumpForce = defaultJumpForce;
        }
    }

    void OnDrawGizmos()
    {
        // DrawLine -> draws from X to Y;
        // In this case, it's drawing from the player position, towards the ground;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
