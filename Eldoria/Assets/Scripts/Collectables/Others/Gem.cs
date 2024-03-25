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
        }
    }

    public void Collect()
    {
        if (player != null)
        {
            player.Inventory.AddGems(gemAmount);

            if (collectParticlePrefab != null)
            {
                Instantiate(collectParticlePrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
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
