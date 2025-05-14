using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScoreController : MonoBehaviour
{
    public TextMeshProUGUI resumenText;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        int puntos1 = PlayerPrefs.GetInt("Puntos_Prueba1", 0);
        int puntos2 = PlayerPrefs.GetInt("Puntos_Prueba2", 0);
        int puntos3 = PlayerPrefs.GetInt("Puntos_Prueba3", 0);
        int puntos4 = PlayerPrefs.GetInt("Puntos_Prueba4", 0);
        int total = puntos1 + puntos2 + puntos3 + puntos4;

        if (resumenText != null)
        {
            resumenText.text = $"RESULTADOS FINALES\n\n" +
                               $"Prueba I: {puntos1}\n" +
                               $"Prueba II: {puntos2}\n" +
                               $"Prueba III: {puntos3}\n" +
                               $"Prueba IV: {puntos4}\n\n" +
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
            SceneManager.LoadScene("Home"); 
        }
    }
}
