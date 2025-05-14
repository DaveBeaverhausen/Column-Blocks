using UnityEngine;

public class PreVR : MonoBehaviour
{
    private string nextSceneName = "PreAR";

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