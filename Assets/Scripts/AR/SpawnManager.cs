using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform[] spawnPoints;
    public float initialSpawnRate = 1.5f; // tiempo inicial entre proyectiles (segundos)
    public float minSpawnRate = 0.3f;     // tiempo mínimo entre proyectiles
    public float acceleration = 0.02f;    // cuánto se reduce el spawnRate cada vez

    private float spawnRate;
    private bool spawning = true;

    void Start()
    {
        spawnRate = initialSpawnRate;
        StartCoroutine(SpawnProjectiles());
    }

    IEnumerator SpawnProjectiles()
    {
        while (spawning)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(spawnRate);
            // Acelera el ritmo de spawn hasta el mínimo permitido
            if (spawnRate > minSpawnRate)
                spawnRate -= acceleration;
        }
    }

    void SpawnProjectile()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(projectilePrefab,
                   spawnPoints[randomIndex].position,
                   Quaternion.LookRotation(Vector3.down));
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
