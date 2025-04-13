using UnityEngine;

public class FinalSceneMusic : MonoBehaviour
{
    public AudioClip musicaFinal;

    void Start()
    {
        if (MusicManager.instancia != null)
        {
            MusicManager.instancia.CambiarMusica(musicaFinal, true); 
        }
    }
}
