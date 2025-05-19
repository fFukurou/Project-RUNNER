using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    
    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

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


        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnDrawGizmos()
    {
        // DrawLine -> draws from X to Y;
        // In this case, it's drawing from the player position, towards the ground;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
