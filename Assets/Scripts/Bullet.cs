using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // player = FindFirstObjectByType<PlayerMovement>(); // Find the PlayerMovement script in the scene, normally not used for big projects because it is computationally expensive
        player = FindAnyObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed; // Get the direction of the player and multiply it by the bullet speed
    }

    void Update()
    {
        myRigidbody.linearVelocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.5f); // Delay before destroying the bullet on collision with any object
    }
}
