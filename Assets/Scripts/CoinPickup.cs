using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100; // The value of the coin

    bool isCollected = false; // Flag to track if the coin has been collected, also to maek sure it is only collected once

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            // FindFirstObjectByType<GameSession>().AddToScore(pointsForCoinPickup); // Obsolete method, replaced with FindAnyObjectByType<GameSession>() for better performance and flexibility
            FindAnyObjectByType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position);
            gameObject.SetActive(false); // Disable the coin object instead of destroying it (greater insurance)
            Destroy(gameObject);
        }
    }
}