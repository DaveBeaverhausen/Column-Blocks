using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VrScript : MonoBehaviour
{
    public TextMeshProUGUI[] puntosTexts;
    public TextMeshProUGUI[] tiempoTexts;
    public float totalTime = 90f;

    private float tiempoActual;
    private int puntos = 0;

    void Start()
    {
        tiempoActual = totalTime;
        ActualizarMarcadores();
    }

    void Update()
    {
        tiempoActual -= Time.deltaTime;
        ActualizarMarcadores();

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            GuardarPuntosYVolver();
        }
    }

    public void SumarPunto()
    {
        puntos++;
        ActualizarMarcadores();
    }

    void ActualizarMarcadores()
    {
        foreach (var texto in puntosTexts)
            texto.text = $"Score: {puntos}";

        foreach (var texto in tiempoTexts)
            texto.text = $"Time: {Mathf.CeilToInt(tiempoActual)}";
    }

    void GuardarPuntosYVolver()
    {
        PlayerPrefs.SetInt("Puntos_Prueba3", puntos);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Home"); // Asegúrate de que el nombre sea correcto
    }
}
