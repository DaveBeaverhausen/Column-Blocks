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
        ActualizarTexto();
    }

    /*public void DecrementarPuntuacion(int puntos)
    {
        score -= puntos;
        ActualizarTexto();
    }*/

    //Metodo para poder pasar la puntuacion de un juego a otro
    public int ObtenerPuntuacion()
    {
        return score;
    }

    // Método para establecer la puntuación
    public void EstablecerPuntuacion(int nuevaPuntuacion)
    {
        score = nuevaPuntuacion;
        ActualizarTexto();
    }

    //Actualizar el marcador
    private void ActualizarTexto()
    {
        textoMarcador.text = $"MARCA\n { score}";
    }
}