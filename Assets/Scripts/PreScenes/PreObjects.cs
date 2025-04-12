using UnityEngine;
using System.Collections;

public class PreObjects : MonoBehaviour
{
    [Header("Configuración")]
    public float waitTime = 3f;                  // Tiempo de espera en segundos
    public string nextScene = "Objects";         // Escena a la que transiciona

    void Start()
    {
        Time.timeScale = 1f; // Por si venimos de un juego pausado
        Debug.Log("⏳ PreObjects: esperando " + waitTime + " segundos...");

        StartCoroutine(CambiarDeEscenaConDelay());
    }

    IEnumerator CambiarDeEscenaConDelay()
    {
        yield return new WaitForSeconds(waitTime);

        if (SceneLoader.Instance != null)
        {
            Debug.Log("🚀 Transición: cargando escena '" + nextScene + "'");
            SceneLoader.Instance.LoadScene(nextScene);
        }
        else
        {
            Debug.LogError("❌ SceneLoader.Instance no encontrado.");
        }
    }
}
