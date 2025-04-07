using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Marcador : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI textoMarcador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        ActualizarTexto();
    }

    public void IncrementarPuntuacion(int puntos)
    {
        score += puntos;
        textoMarcador.text = score.ToString();
    }

    public void DecrementarPuntuacion(int puntos)
    {
        score -= puntos;
        textoMarcador.text = score.ToString();
    }

    private void ActualizarTexto()
    {
        textoMarcador.text = score.ToString(); // Actualiza el texto con la puntuación actual
    }
}
