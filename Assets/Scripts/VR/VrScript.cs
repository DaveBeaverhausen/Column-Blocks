using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VrScript : MonoBehaviour
{
    public TextMeshProUGUI[] puntosTexts;
    public TextMeshProUGUI[] tiempoTexts;
    public float totalTime = 90f;
    public int golpesMaximos = 5;
    public TextMeshProUGUI mensajeFinal;

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
        PlayerPrefs.SetInt("Puntos_Prueba3", puntos);
        PlayerPrefs.Save();

          CargarMenu();
        
    }

    void FinalizarJuego1()
    {
        juegoActivo = false;
        PlayerPrefs.SetInt("Puntos_Prueba3", puntos);
        PlayerPrefs.Save();

          PantallaFinal();
        
    }

    void CargarMenu()
    {
        SceneManager.LoadScene("Home");
    }

    void PantallaFinal()
    {
        SceneManager.LoadScene("FinalScene");
    }
}
