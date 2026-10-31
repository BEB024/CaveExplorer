using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D playerRigibody;
    Animator playerAnimator;

    void Start()
    {
        playerRigibody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move has activated" + moveInput);
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

}
