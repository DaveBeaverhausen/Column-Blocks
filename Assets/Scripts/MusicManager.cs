using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton: Solo una instancia
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
}
