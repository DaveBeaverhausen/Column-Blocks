using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives = 5;
    public float gameTime = 90f;
    public int score = 0;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    void Awake() => Instance = this;

    void Start()
    {
        livesText.text = $"Vidas: {lives}";
        scoreText.text = $"Marca: {score}";
    }

    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60f);
            int seconds = Mathf.FloorToInt(gameTime % 60f);
            timerText.text = $"Tiempo\n {minutes:00}:{seconds:00}";
            if (gameTime <= 0) WinGame();
        }
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = $"Marca: {score}";
    }

    public void TakeDamage()
    {
        lives--;
        livesText.text = $"Vidas: {lives}";

        // Aumenta la velocidad del escudo al perder una vida
        ShieldController shieldController = FindFirstObjectByType<ShieldController>();
        if (shieldController != null)
        {
            shieldController.IncreaseSpeed(0.5f); // Aumenta el multiplicador en 0.5
        }

        if (lives <= 0) GameOver();
    }

    void WinGame()
    {
        EndGame(); // Usa el método común para finalizar
        PlayerPrefs.SetInt("Puntos_Prueba3", score); // Guarda solo en victoria
        PlayerPrefs.Save();
    }

    void GameOver()
    {
        EndGame(); // Usa el mismo método para derrota
    }

    void EndGame()
    {
        // Detiene el juego y muestra el panel
        FindFirstObjectByType<SpawnManager>().StopSpawning();
        gameOverPanel.SetActive(true);
        scoreText.text = $"MARCA: {score}";
        Time.timeScale = 0f; // Pausa el juego (opcional)
    }
}