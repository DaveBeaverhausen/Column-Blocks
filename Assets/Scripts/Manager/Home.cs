using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public TextMeshProUGUI puntosText; // Texto donde se mostrarán los resultados
    public AudioClip musicaHome;

    void Start()
    {
        if (MusicManager.instancia != null && musicaHome != null)
        {
            MusicManager.instancia.CambiarMusica(musicaHome, true);
        }

        int puntos1 = PlayerPrefs.GetInt("Puntos_Prueba1", 0);
        int puntos2 = PlayerPrefs.GetInt("Puntos_Prueba2", 0);
        int puntos3 = PlayerPrefs.GetInt("Puntos_Prueba3", 0);
        int puntos4 = PlayerPrefs.GetInt("Puntos_Prueba4", 0);
        int total = puntos1 + puntos2 + puntos3 + puntos4;

        if (puntosText != null)
        {
            puntosText.text = $"Prueba I: {puntos1}\nPrueba II: {puntos2}\nPrueba III: {puntos3}\nPrueba IV: {puntos4}\nTotal: {total}";
        }
    }

    public void GoToNextScene()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instance no encontrado.");
            return;
        }

        SceneLoader.Instance.LoadScene("PreObjects");
    }

    public void ReiniciarPuntos()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}
