using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectorController : MonoBehaviour
{
    public void IrAEspartano()
    {
        LoadSceneSafely("PreObjects");
    }

    public void IrAColumnas()
    {
        LoadSceneSafely("PreColumns");
    }

    public void IrAVR()
    {
        LoadSceneSafely("PreVR");
    }

    public void IrAAR()
    {
        LoadSceneSafely("PreAR");
    }

    private void LoadSceneSafely(string sceneName)
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneLoader.Instance no encontrado, usando SceneManager directamente");
            SceneManager.LoadScene(sceneName);
        }
    }
}