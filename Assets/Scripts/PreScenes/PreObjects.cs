using UnityEngine;
using System.Collections;

public class PreObjects : MonoBehaviour
{
    [Header("Configuración")]
    public float waitTime = 3f;                  
    public string nextScene = "Objects";       

    void Start()
    {
        Time.timeScale = 1f; 
        Debug.Log("⏳ PreObjects: esperando " + waitTime + " segundos...");

        StartCoroutine(CambiarDeEscenaConDelay());
    }

    IEnumerator CambiarDeEscenaConDelay()
    {
        yield return new WaitForSeconds(waitTime);

        if (SceneLoader.Instance != null)
        {
            Debug.Log("Transición: cargando escena '" + nextScene + "'");
            SceneLoader.Instance.LoadScene(nextScene);
        }
        else
        {
            Debug.LogError("SceneLoader.Instance no encontrado.");
        }
    }
}
