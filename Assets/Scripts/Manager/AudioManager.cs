using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips")]
    public AudioClip sonidoGameOver;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f; 
        audioSource.volume = 1f;
        audioSource.ignoreListenerPause = true;
    }

    public void ReproducirSonidoGameOver()
    {
        if (sonidoGameOver != null)
        {
            audioSource.PlayOneShot(sonidoGameOver);
        }
        else
        {
            Debug.LogWarning("sonidoGameOver no asignado en el AudioManager.");
        }
    }

    [Header("UI Sounds")]
    public AudioClip sonidoClickBoton;

    public void ReproducirSonidoClick()
    {
        if (sonidoClickBoton != null)
        {
            audioSource.PlayOneShot(sonidoClickBoton);
        }
        else
        {
            Debug.LogWarning("sonidoClickBoton no asignado.");
        }
    }
}
