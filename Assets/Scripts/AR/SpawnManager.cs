using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] projectilePrefabs;
    public Transform spawnPoint;
    public Transform shieldTransform;

    private GameObject currentProjectile;
    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnProjectilesRoutine());
    }

    IEnumerator SpawnProjectilesRoutine()
    {
        while (isSpawning)
        {
            // Destruye cualquier proyectil existente antes de generar uno nuevo
            if (currentProjectile != null) Destroy(currentProjectile);

            yield return new WaitForSeconds(0.5f); // Pequeño delay para evitar conflictos

            // Genera nuevo proyectil aleatorio
            int prefabIndex = Random.Range(0, projectilePrefabs.Length);
            currentProjectile = Instantiate(
                projectilePrefabs[prefabIndex],
                spawnPoint.position,
                Quaternion.identity
            );

            // Configuración inicial y desactivación temporal
            currentProjectile.SetActive(false);

            Projectile projectileScript = currentProjectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(this, shieldTransform);
            }

            yield return new WaitUntil(() => currentProjectile == null); // Espera hasta que el proyectil actual sea destruido
        }
    }

    public void ProjectileDestroyed()
    {
        currentProjectile = null;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}