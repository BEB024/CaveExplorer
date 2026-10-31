using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigibody;
    [SerializeField] float playerSpeed = 10f;

    void Start()
    {
     playerRigibody = GetComponent<Rigidbody2D>();
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
