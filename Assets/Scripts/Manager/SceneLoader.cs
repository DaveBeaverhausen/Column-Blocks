using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

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
}
