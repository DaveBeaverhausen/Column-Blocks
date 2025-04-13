using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    public void ReproducirSonidoBoton()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ReproducirSonidoClick();
        }
        else
        {
            Debug.LogWarning("No se encontró AudioManager.Instance");
        }
    }
}
