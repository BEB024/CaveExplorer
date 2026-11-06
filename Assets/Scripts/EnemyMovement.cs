using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 1f;
    Rigidbody2D enemyRigidbody;
    BoxCollider2D wallCollider;


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        wallCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        enemyRigidbody.linearVelocity = new Vector2(enemySpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        enemySpeed = -enemySpeed; // Reverse the direction of the enemy when it hits a trigger
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.linearVelocity.x)), 1f); // Flip the enemy's facing direction based on its velocity
    }
}
