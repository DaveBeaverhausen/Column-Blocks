using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScoreController : MonoBehaviour
{
    public TextMeshProUGUI resumenText;

    void Start()
    {
        int puntos1 = PlayerPrefs.GetInt("Puntos_Prueba1", 0);
        int puntos2 = PlayerPrefs.GetInt("Puntos_Prueba2", 0);
        int total = puntos1 + puntos2;

        if (resumenText != null)
        {
            resumenText.text = $"RESULTADOS FINALES\n\n" +
                               $"Prueba I: {puntos1}\n" +
                               $"Prueba II: {puntos2}\n\n" +
                               $"Total: {total}";
        }
    }

    public void IrAMenu()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene("Home");
        }
        else
        {
            SceneManager.LoadScene("Home"); // Fallback
        }
    }
}
