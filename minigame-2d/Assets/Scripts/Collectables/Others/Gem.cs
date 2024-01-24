using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
    public int gemAmount=1;
    public ParticleSystem collectParticlePrefab;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogWarning("Player not found. Gem collection may not work as expected.");
        }
    }

    public void Collect()
    {
        if (player != null)
        {
            player.Inventory.AddGems(gemAmount);
            Debug.Log($"Collected {gemAmount} gems. Total: {player.Inventory.GemAmount}");

            if (collectParticlePrefab != null)
            {
                Instantiate(collectParticlePrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Player not found. Gem collection failed.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
}
