using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;
    private AudioSource audioSource;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarMusica(AudioClip nuevaMusica, bool loop = true)
    {
        if (audioSource == null || nuevaMusica == null) return;

        if (audioSource.clip == nuevaMusica) return;

        audioSource.Stop();
        audioSource.clip = nuevaMusica;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void PararMusica()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
