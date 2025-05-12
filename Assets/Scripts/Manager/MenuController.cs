using UnityEngine;
using System.Collections; 

public class MenuController : MonoBehaviour
{
    public string nextSceneName = "Home";
    public float waitTime = 4f;

    void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        GoToNextScene();
    }

    private void GoToNextScene()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instance no encontrado.");
            return;
        }

        SceneLoader.Instance.LoadScene(nextSceneName);
    }

}

/**
*Usamos coroutine, que nos permite esperar el tiempo propuesto(7segundos) y cambiar de escena
*Para usar la herramienta coroutine, usamos System.Collections
**/
