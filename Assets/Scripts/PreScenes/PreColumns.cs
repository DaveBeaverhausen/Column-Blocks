using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class PreColumns : MonoBehaviour
{
    private string nextSceneName = "SampleScene";
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

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

/**
*Usamos coroutine, que nos permite esperar el tiempo propuesto(7segundos) y cambiar de escena
*Para usar la herramienta coroutine, usamos System.Collections
**/
