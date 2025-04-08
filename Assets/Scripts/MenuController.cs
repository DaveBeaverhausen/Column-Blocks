using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoToNextScene()
    {
        SceneManager.LoadScene("Objects"); 
    }
}