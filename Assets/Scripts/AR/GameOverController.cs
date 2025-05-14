using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void IrAFinalScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FinalScene");
    }

    public void VolverASelectorJuegos()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameSelector");
    }
}