using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the animator component for controlling animations
    public Animator animator;

    // Serialized fields accessible from the Unity Editor
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D component for physics
    [SerializeField] private Transform groundCheck; // Transform to check if the player is grounded
    [SerializeField] private LayerMask groundLayer; // LayerMask to identify ground objects
    [SerializeField] private LayerMask climbLayer; // LayerMask to identify walls for climbing

    // Booleans to track various states of the player
    private bool isRunning;
    private bool isJumping;
    private bool isWallSlidingBool;
    private bool isFacingRight = true;

    // Variables controlling movement
    private float horizontal;
    private float speed = 5f;
    private float jumpPower = 16f;
    private float wallSlideSpeed = 1f;
    private float wallJumpForce = 3f;

    // Counter for wall climbing
    private int climbCount = 1;

    private void Update()
    {
        // Handle input for movement, jumping, and update animator
        HandleMovementInput();
        HandleJumpInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        // Move the player and handle wall sliding
        MovePlayer();
        HandleWallSlide();
    }

    // Handles horizontal movement input
    private void HandleMovementInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        // Check if player is not jumping to determine if they are running
        if (!isJumping) { isRunning = Mathf.Abs(horizontal) > 0f; }
    }

    // Handles jump input
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded())
            {
                Jump(); // Execute jump if grounded
            }
            else if (isWallSliding() && climbCount < 2)
            {
                WallJump(); // Execute wall jump if wall sliding and climb count is less than 2
            }
        }
    }

    // Makes the player jump
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        climbCount = 1;
        isJumping = true;
    }

    // Makes the player execute a wall jump
    private void WallJump()
    {
        rb.velocity = new Vector2(-transform.localScale.x * wallJumpForce, jumpPower);
        climbCount++;
    }

    // Moves the player horizontally
    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        Flip(); // Flip the player's sprite if changing direction
    }

    // Handles the player's behavior during wall sliding
    private void HandleWallSlide()
    {
        if (isWallSliding())
        {
            // Limit the player's vertical velocity during wall slide
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            isWallSlidingBool = true;
        }
        else { isWallSlidingBool = false; }
        animator.SetBool("isWallSliding", isWallSlidingBool); // Update animator parameter for wall sliding
    }

    // Update animator parameters for running and jumping
    private void UpdateAnimator()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        isJumping = false; // Reset jump state after updating animator
    }

    // Flip the player's sprite based on movement direction
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Check if the player is grounded
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Check if the player is wall sliding
    private bool isWallSliding()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.5f, climbLayer)
            && !isGrounded();
    }
}
