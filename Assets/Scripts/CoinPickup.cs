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
            FindAnyObjectByType<GameSession>().AddToScore(pointsForCoinPickup); // FindFirstObjectByType<GameSession>().AddToScore(pointsForCoinPickup) was replaced for better performance
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position);
            gameObject.SetActive(false); // Disable the coin object instead of destroying it (greater insurance)
            Destroy(gameObject);
        }
    }
}