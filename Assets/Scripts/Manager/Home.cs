using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public TextMeshProUGUI puntosText; // Texto donde se mostrarán los resultados

    void Start()
    {
        int puntos1 = PlayerPrefs.GetInt("Puntos_Prueba1", 0);
        int puntos2 = PlayerPrefs.GetInt("Puntos_Prueba2", 0);
        int total = puntos1 + puntos2;

        if (puntosText != null)
        {
            puntosText.text = $"Prueba I: {puntos1}\nPrueba II: {puntos2}\nTotal: {total}";
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
