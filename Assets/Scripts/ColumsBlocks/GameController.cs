using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Instancias")]
    public GameObject bloquePrefab;
    public Transform gancho;
    public Transform puntoCreacionBloque;
    public GameObject gameOverPanel;

    [Header("UI")]
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoMarca;

    [Header("Control")]
    public float tiempoRestante = 90f;
    private int puntuacion = 0;
    private bool juegoTerminado = false;
    private bool bloqueEsperandoAsentarse = false;

    [Header("Transición")]
    public string nextSceneName = "ErrorVR";
    public CameraFollow cameraFollow;
    public Gancho ganchoScript;
    public float incrementoVelocidad = 0.5f;
    public float velocidadMaxima = 10f;

    [Header("Audio")]
    public AudioClip sonidoGameOver; // Clip de sonido para Game Over
    private AudioSource audioSource; // Fuente de audio

    private GameObject bloqueActual;

    void Start()
    {
        // Inicializar componentes
        audioSource = GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        puntuacion = 0;

        ActualizarUI();
        CrearNuevoBloque();

        Time.timeScale = 1; // Asegurarse de que el tiempo esté activo
    }

    void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            GameOver();
        }

        if (Input.GetMouseButtonDown(0) && bloqueActual != null && !bloqueEsperandoAsentarse)
        {
            SoltarBloque();
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoPuntuacion.text = $"MARCA\n{puntuacion}";
        textoTiempo.text = $"TIEMPO\n{Mathf.FloorToInt(tiempoRestante / 60):00}:{Mathf.FloorToInt(tiempoRestante % 60):00}";
    }

    void CrearNuevoBloque()
    {
        bloqueActual = Instantiate(bloquePrefab, puntoCreacionBloque.position, Quaternion.identity);
        bloqueActual.transform.parent = gancho;
        bloqueActual.GetComponent<Rigidbody2D>().simulated = false;

        Bloque bloqueScript = bloqueActual.GetComponent<Bloque>();
        if (bloqueScript != null)
            bloqueScript.gameController = this;

        bloqueEsperandoAsentarse = false;
    }

    void SoltarBloque()
    {
        bloqueActual.transform.parent = null;
        Rigidbody2D rb = bloqueActual.GetComponent<Rigidbody2D>();
        rb.simulated = true;

        cameraFollow.SetUltimoBloque(bloqueActual.transform);

        ganchoScript.velocidadInicial = Mathf.Min(ganchoScript.velocidadInicial + incrementoVelocidad, velocidadMaxima);

        bloqueEsperandoAsentarse = true;
        bloqueActual = null;
    }

    public void BloqueAsentado()
    {
        if (juegoTerminado) return;

        bloqueEsperandoAsentarse = false;
        Invoke(nameof(CrearNuevoBloque), 0.5f);
    }

    public void SumarPunto()
    {
        puntuacion++;
        ActualizarUI();
    }

    public void GameOver()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;

        PlayerPrefs.SetInt("Puntos_Prueba2", puntuacion); // Guardar puntuación
        PlayerPrefs.Save();

        if (textoMarca != null)
            textoMarca.text = $"Marca: {puntuacion}";

        gameOverPanel.SetActive(true);

        // Reproducir sonido de Game Over
        if (AudioManager.Instance != null)
            AudioManager.Instance.ReproducirSonidoGameOver();

        else if (audioSource != null && sonidoGameOver != null)
            audioSource.PlayOneShot(sonidoGameOver);

        Time.timeScale = 0; // Detener el tiempo
    }

    public void IrASiguienteEscena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextSceneName);
    }
}
