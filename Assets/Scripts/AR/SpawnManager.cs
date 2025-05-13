using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // Piedra, lanza, espada
    public Transform spawnPoint; // Punto central para que aparezcan los proyectiles
    public Transform shieldTransform; // Escudo objetivo
    public float initialSpawnDelay = 1.5f;
    public float minSpawnDelay = 0.3f;
    public float speedIncrease = 0.05f;

    private float currentSpawnDelay;
    private bool isSpawning = true;
    private bool projectileActive = false; // Controla si hay un proyectil activo

    void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        // Elimina cualquier proyectil existente al iniciar
        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(projectile);
        }
        StartCoroutine(SpawnProjectilesRoutine());
    }

    IEnumerator SpawnProjectilesRoutine()
    {
        while (isSpawning)
        {
            // Solo genera un nuevo proyectil si no hay uno activo
            if (!projectileActive)
            {
                SpawnProjectile();
            }
            yield return new WaitForSeconds(0.5f); // Comprueba periódicamente
        }
    }

    void SpawnProjectile()
    {
        // Elegir UN tipo de proyectil aleatorio (no todos a la vez)
        int prefabIndex = Random.Range(0, projectilePrefabs.Length);
        GameObject prefab = projectilePrefabs[prefabIndex];

        // Posición central para apuntar
        Vector3 spawnPos = spawnPoint.position;

        // El proyectil aparece pero NO se mueve automáticamente
        GameObject projectile = Instantiate(
            prefab,
            spawnPos,
            Quaternion.identity
        );

        // Añadir una referencia al SpawnManager
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.spawnManager = this;
            // NO inicializar la dirección ni velocidad aquí
        }

        projectileActive = true;
    }

    public void IncreaseDifficulty(float amount)
    {
        // Reduce el tiempo de espera para aumentar la dificultad
        currentSpawnDelay = Mathf.Max(currentSpawnDelay - amount, minSpawnDelay);
    }

    // Llamado cuando un proyectil se destruye
    public void ProjectileDestroyed()
    {
        projectileActive = false;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}