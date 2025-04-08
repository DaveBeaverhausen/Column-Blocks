using UnityEngine;

public class CaidaFrutas : MonoBehaviour
{
    public GameObject fruitPrefab; // Prefab de la fruta
    public float spawnInterval = 1f; // Intervalo entre frutas
    public float spawnHeight = 5f; // Altura donde aparecen
    public float spawnRangeX = 2f; // Rango horizontal
    private bool gameActive = true;

    void Start()
    {
        InvokeRepeating("SpawnFruit", 0f, spawnInterval);
    }

    void Caida()
    {
        if (!gameActive) return;

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);
        GameObject newFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // Con esto activo la gravedad 2d para la caida de las frutas
    }

    public void StopCaida()
    {
        gameActive = false;
    }
}