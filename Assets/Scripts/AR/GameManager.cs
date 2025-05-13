using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives = 3;
    public float gameTime = 90f;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text livesText;

    void Awake() => Instance = this;

    void Start()
    {
        livesText.text = $"Vidas: {lives}";
    }

    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60f);
            int seconds = Mathf.FloorToInt(gameTime % 60f);
            timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";
            if (gameTime <= 0) WinGame();
        }
    }

    public void TakeDamage()
    {
        lives--;
        livesText.text = $"Vidas: {lives}";
        if (lives <= 0) GameOver();
    }

    void WinGame()
    {
        FindFirstObjectByType<SpawnManager>().StopSpawning();
        Debug.Log("¡Victoria! Esparta sobrevive");
    }

    void GameOver()
    {
        FindFirstObjectByType<SpawnManager>().StopSpawning();
        Debug.Log("¡Derrota! Mejora tu defensa");
    }
}