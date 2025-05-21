using UnityEngine;
using System.Collections;

public class PreAR : MonoBehaviour
{
    private string nextSceneName = "ARVelocidad";
    public float waitTime = 8f;

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
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("SceneLoader.Instance no encontrado, usando SceneManager directamente");
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}