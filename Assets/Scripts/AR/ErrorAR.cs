using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class ErrorAR : MonoBehaviour
{
    private string nextSceneName = "FinalScene";

    public void GoToNextScene()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("SceneLoader.Instance no encontrado.");
        }
    }
}