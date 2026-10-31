using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 7f;
    [SerializeField] float playerJumpSpeed = 15f;
    [SerializeField] float playerClimbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D playerRigibody;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    float playerStartGravity;

    void Start()
    {
        playerRigibody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        playerStartGravity = playerRigibody.gravityScale; // Store the original gravity scale of the player
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // Debug.Log("Move has activated" + moveInput);
    }

    void OnJump(InputValue value)
    {
        // Check if the player is not touching the ground layer (to prevent double jumping)
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; } // Exit the function if the player is not on the ground
        if (value.isPressed) // Check if the jump button (what is assigned to it in the input actions) is pressed (it is specifically for Jump due to OnJump being called when the jump button is pressed)
        {
            // Debug.Log("Jump has activated");
            playerRigibody.linearVelocity += new Vector2(0f, playerJumpSpeed); // Add a vertical velocity to the player when jumping  

        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, playerRigibody.linearVelocity.y);
        playerRigibody.linearVelocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(playerRigibody.linearVelocity.x) > Mathf.Epsilon; // Reuse the hasHorizontalSpeed variable to check if the player is moving horizontally
        playerAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(playerRigibody.linearVelocity.x) > Mathf.Epsilon;
        /* Epsilon is a very small number, used to check if the value is close to zero, so here the sign does not matter. 
           Instead it sees if the player is moving or not, no matter how minimal.*/
        if (hasHorizontalSpeed) 
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigibody.linearVelocity.x), 1f); 
            // If the player is moving to the right, the sign will be positive, if moving left, it will be negative. This will flip the sprite.
        }
    }

    void ClimbLadder()
    {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            playerRigibody.gravityScale = playerStartGravity;
            playerAnimator.SetBool("isClimbing", false);
            return; // Exit the function if the player is not touching the climbing layer
        }
        else
        {
            Vector2 climbVelocity = new Vector2(playerRigibody.linearVelocity.x, moveInput.y * playerClimbSpeed);
            playerRigibody.linearVelocity = climbVelocity;
            playerRigibody.gravityScale = 0;

            bool playerClimbing = Mathf.Abs(playerRigibody.linearVelocity.y) > Mathf.Epsilon; // Check if the player is climbing (moving vertically)
            playerAnimator.SetBool("isClimbing", playerClimbing);
        }
    }

}
