using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class ErrorAR : MonoBehaviour
{
    private string nextSceneName = "Home";

    /*public float waitTime = 4f;

    void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        GoToNextScene();
    }
    */

    public void GoToNextScene()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("❌ SceneLoader.Instance no encontrado.");
        }
    }
}

/**
*Usamos coroutine, que nos permite esperar el tiempo propuesto(7segundos) y cambiar de escena
*Para usar la herramienta coroutine, usamos System.Collections
**/
