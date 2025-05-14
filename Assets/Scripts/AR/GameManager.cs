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

    [Header("Game Over")]
    public TMP_Text gameOverScoreText;

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
        if (lives <= 0) return;

        // Resta 1 vida
        lives--;
        lives = Mathf.Max(lives, 0);
        livesText.text = $"Vidas: {lives}";

        if (lives <= 0) GameOver();
    }

    void WinGame()
    {
        EndGame();
    }

    void GameOver()
    {
        EndGame();
    }

    void EndGame()
    {
        FindFirstObjectByType<SpawnManager>().StopSpawning();
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = $"MARCA: {score}";

        // Guarda los puntos siempre al finalizar (victoria o derrota)
        PlayerPrefs.SetInt("Puntos_Prueba4", score);
        PlayerPrefs.Save();

        Time.timeScale = 0f;
    }
}