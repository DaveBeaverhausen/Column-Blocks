using UnityEngine;

public class ErrorVR : MonoBehaviour
{
    private string nextSceneName = "ErrorAR";

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


