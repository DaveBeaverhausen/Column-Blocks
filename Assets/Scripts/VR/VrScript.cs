using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VrScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public float totalTime = 90f;
    private float currentTime;
    private int score;

    void Start()
    {
        currentTime = totalTime;
        UpdateUI();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            FinalizarJuego();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (timeText != null)
            timeText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }

    public void SumarPunto()
    {
        score++;
        PlayerPrefs.SetInt("Puntos_Prueba3", score); // Guardar para HomeController
        UpdateUI();
    }

    void FinalizarJuego()
    {
        SceneManager.LoadScene("HomeScene"); // Cambia por el nombre real de tu escena Home
    }
}
