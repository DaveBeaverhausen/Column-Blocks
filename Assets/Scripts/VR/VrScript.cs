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
            FinalizarJuego();
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
        if (!juegoActivo) return;
        juegoActivo = false;

        // Detener generación de frutas y ladrillos
        foreach (CaidaFruta cf in FindObjectsOfType<CaidaFruta>())
            cf.PararGeneracion();
        foreach (Ladrillo l in FindObjectsOfType<Ladrillo>())
            l.PararGeneracion();

        // Mostrar Game Over y guardar puntos
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        if (gameOverScoreText != null)
            gameOverScoreText.text = $"MARCA: {puntos}";
        PlayerPrefs.SetInt("Puntos_Prueba3", puntos);
        PlayerPrefs.Save();

        Transform textoFin = gameOverPanel.transform.Find("TextoFin");
        if (textoFin != null)
            textoFin.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void VolverASelectorJuegos()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameSelector");
    }

    public void IrASiguienteEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PreAR");
    }
}
