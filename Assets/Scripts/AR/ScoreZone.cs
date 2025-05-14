using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            // Solo notifica al SpawnManager y destruye el proyectil
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null && projectile.spawnManager != null)
            {
                projectile.spawnManager.ProjectileDestroyed();
            }

            Destroy(other.gameObject);
        }
    }
}