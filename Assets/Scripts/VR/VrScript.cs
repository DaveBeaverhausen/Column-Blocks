using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class VrScript : MonoBehaviour
{
    public TextMeshProUGUI[] puntosTexts;
    public TextMeshProUGUI[] tiempoTexts;
    public float totalTime = 90f;
    public int golpesMaximos = 5;
    public TextMeshProUGUI mensajeFinal;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;

    private float tiempoActual;
    private int puntos = 0;
    private int golpes = 0;
    private bool juegoActivo = true;

    void Start()
    {
        tiempoActual = totalTime;
        ActualizarMarcadores();

        if (mensajeFinal != null)
            mensajeFinal.gameObject.SetActive(false); // Ocultar mensaje al iniciar

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!juegoActivo) return;

        tiempoActual -= Time.deltaTime;
        ActualizarMarcadores();

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            FinalizarJuego1();
        }
    }

    public void SumarPunto()
    {
        if (!juegoActivo) return;

        puntos++;
        ActualizarMarcadores();
    }

    public void RecibirGolpe()
    {
        if (!juegoActivo) return;

        puntos = Mathf.Max(0, puntos - 1);
        golpes++;

        ActualizarMarcadores();

        if (golpes >= golpesMaximos)
        {
            FinalizarJuego();
        }
    }

    void ActualizarMarcadores()
    {
        foreach (var texto in puntosTexts)
            if (texto != null)
                texto.text = $"Score: {puntos}";

        foreach (var texto in tiempoTexts)
            if (texto != null)
                texto.text = $"Time: {Mathf.CeilToInt(tiempoActual)}";
    }

    void FinalizarJuego()
    {
        juegoActivo = false;

        // Mostrar Game Over y guardar puntos
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = $"PUNTOS: {puntos}";
        PlayerPrefs.SetInt("Puntos_Prueba3", puntos);
        PlayerPrefs.Save();

        Time.timeScale = 0f; // Pausar el juego
        StartCoroutine(CargarEscenaFinal());
    }

    void FinalizarJuego1()
    {
        juegoActivo = false;

        // Mostrar Game Over y guardar puntos
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = $"PUNTOS: {puntos}";
        PlayerPrefs.SetInt("Puntos_Prueba3", puntos);
        PlayerPrefs.Save();

        Time.timeScale = 0f; // Pausar el juego
        StartCoroutine(CargarEscenaFinal());
    }

    IEnumerator CargarEscenaFinal()
    {
        yield return new WaitForSecondsRealtime(2f); // Esperar 2 segundos (tiempo real aunque el juego esté pausado)
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene("GameOverVR"); // Cambiar a tu escena final de VR
    }
}
