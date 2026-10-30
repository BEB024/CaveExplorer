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
}
