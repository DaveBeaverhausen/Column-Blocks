using UnityEngine;
using UnityEngine.SceneManagement;

public class PreVR : MonoBehaviour
{
    public float tiempoEspera = 5f; 
    public string siguienteEscena = "FruitVR";

    void Start()
    {
        Invoke(nameof(CargarSiguienteEscena), tiempoEspera);
    }

    void CargarSiguienteEscena()
    {
        SceneManager.LoadScene(siguienteEscena);
    }
}
