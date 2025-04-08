using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FruitNinjaVR : MonoBehaviour
{
    public GameObject fruitPrefab; // Prefab de la fruta
    public float spawnInterval = 1f; // Intervalo entre frutas
    public float spawnHeight = 5f; // Altura donde aparecen las frutas
    public float spawnRangeX = 2f; // Rango horizontal de aparición de las frutas
    public float gameTime = 60f; // Tiempo total de juego (1 minuto)
    public TextMeshProUGUI timerText; // Texto para mostrar el temporizador

    private float timer; // Contador de tiempo
    private bool gameActive = true; // Estado del juego

    void Start()
    {
        timer = gameTime;
        InvokeRepeating("SpawnFruit", 0f, spawnInterval); // Empieza a generar frutas cada intervalo
    }

    void Update()
    {
        if (!gameActive) return;

        // Reducir el tiempo en cada frame
        timer -= Time.deltaTime;

        // Si el tiempo se acaba, termina el juego
        if (timer <= 0)
        {
            timer = 0;
            gameActive = false;
            SceneManager.LoadScene("NextGame"); // Cambia a la siguiente escena del juego
        }

        // Actualizar el texto del temporizador
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timer);
            timerText.text = "Tiempo: " + seconds.ToString();
        }
    }

void SpawnFruit()
{
    if (!gameActive) return;

    // Generar posición aleatoria para la fruta en el eje X
    float randomX = Random.Range(-spawnRangeX, spawnRangeX);
    Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f); // Genera la fruta arriba

    // Instanciar la fruta
    GameObject newFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);

    // Configurar el Rigidbody de la fruta para que caiga con la gravedad 3D
    Rigidbody rb = newFruit.GetComponent<Rigidbody>();
    if (rb == null)
    {
        rb = newFruit.AddComponent<Rigidbody>();  // Si no tiene Rigidbody, lo añadimos
    }
    rb.useGravity = true; // Habilitar la gravedad 3D
}



}
