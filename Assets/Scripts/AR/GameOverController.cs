using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void IrAFinalScene()
    {
        SceneManager.LoadScene("Final Scene");
    }
}