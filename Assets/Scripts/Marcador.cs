using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Marcador : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI textoMarcador;


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
        textoMarcador.text = score.ToString(); 
    }
}
