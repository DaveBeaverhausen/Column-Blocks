using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Necesario para usar TextMeshPro

public class HomeController : MonoBehaviour
{
    public int puntos = 0; // Variable para almacenar los puntos, inicializada en 0
    public TextMeshProUGUI puntosText; // Referencia al texto que mostrará los puntos

    void Start()
    {
        // Asegurarse de que los puntos empiecen en 0
        puntos = 0;
        ActualizarTextoPuntos(); // Mostrar los puntos iniciales en el texto
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene("Objects");
    }

    // Método para actualizar el texto de los puntos
    private void ActualizarTextoPuntos()
    {
        if (puntosText != null)
        {
            puntosText.text = "Last Score: " + puntos.ToString();
        }
    }

   
}