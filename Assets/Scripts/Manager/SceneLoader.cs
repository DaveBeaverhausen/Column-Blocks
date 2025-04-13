using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private bool isLoading = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Cambiando a escena: {sceneName}");

        string escenaActual = SceneManager.GetActiveScene().name;

        if (escenaActual == sceneName)
        {
            Debug.Log("Escena actual es la misma que se intenta cargar. Forzando recarga.");
            SceneManager.LoadScene(sceneName); 
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private System.Collections.IEnumerator LoadSceneCoroutine(string sceneName, float delay)
    {
        isLoading = true;

        if (delay > 0)
        {
            Debug.Log("Esperando " + delay + " segundos antes de cambiar a escena: " + sceneName);
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("Cambiando a escena: " + sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        isLoading = false;
    }
}
