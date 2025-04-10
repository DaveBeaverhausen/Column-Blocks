using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public AudioClip sonidoGameOver;
    private AudioSource audioSource;
    public TextMeshProUGUI textoMarca;

    [Header("Control")]
    public float tiempoRestante = 90f;
    private int puntuacion = 0;
    private bool juegoTerminado = false;
    private bool bloqueEsperandoAsentarse = false; // flag

    [Header("Transición al finalizar el tiempo")]
    public string nextSceneName = "ErrorVR"; // Nombre de la escena a la que cambiar cuando se acabe el tiempo

    public CameraFollow cameraFollow;
    public Gancho ganchoScript;
    public float incrementoVelocidad = 0.5f;
    public float velocidadMaxima = 10f;

    private GameObject bloqueActual;

    void Start()
    {
        // Cargar los puntos acumulados al inicio
        puntuacion = PlayerPrefs.GetInt("PuntosAcumulados", 0); // 0 es el valor por defecto si no existe

        gameOverPanel.SetActive(false);
        ActualizarUI();
        CrearNuevoBloque();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (Input.GetMouseButtonDown(0) && bloqueActual != null && !bloqueEsperandoAsentarse)
        {
            SoltarBloque();
        }

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            ChangeSceneOnTimeEnd(); // Cambiar de escena cuando se acabe el tiempo
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntuacion != null)
            textoPuntuacion.text = "SCORE\n" + puntuacion;

        if (textoTiempo != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
            textoTiempo.text = $"TIME\n{minutos:00}:{segundos:00}";
        }
    }

    void CrearNuevoBloque()
    {
        bloqueActual = Instantiate(bloquePrefab, puntoCreacionBloque.position, Quaternion.identity);
        bloqueActual.transform.parent = gancho;
        bloqueActual.GetComponent<Rigidbody2D>().simulated = false;

        bloqueActual.GetComponent<Bloque>().gameController = this;

        bloqueEsperandoAsentarse = false; // Por si viene de GameOver en pausa
    }

    void SoltarBloque()
    {
        bloqueActual.transform.parent = null;
        Rigidbody2D rb = bloqueActual.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.linearVelocity = Vector2.zero;

        if (cameraFollow != null)
            cameraFollow.SetUltimoBloque(bloqueActual.transform);

        if (ganchoScript != null)
            ganchoScript.velocidad = Mathf.Min(ganchoScript.velocidad + incrementoVelocidad, velocidadMaxima);

        bloqueEsperandoAsentarse = true; // no se puede lanzar hasta que avise
        bloqueActual = null;
    }

    public void BloqueAsentado()
    {
        if (juegoTerminado) return;

        bloqueEsperandoAsentarse = false;
        Invoke(nameof(CrearNuevoBloque), 0.5f);
    }

    public void GameOver()
    {
        juegoTerminado = true;

        PlayerPrefs.SetInt("Puntos_Prueba1", puntuacion); // Guardar puntuación de esta prueba
        PlayerPrefs.Save();

        if (textoMarca != null)
        {
            textoMarca.text = "Marca: " + puntuacion;
        }

        gameOverPanel.SetActive(true);

        audioSource.PlayOneShot(sonidoGameOver);

        Time.timeScale = 0;
    }

    public void Reintentar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    public void SumarPunto()
    {
        puntuacion++;
        ActualizarUI();
    }

    private void ChangeSceneOnTimeEnd()
    {
        juegoTerminado = true;

        
        PlayerPrefs.SetInt("PuntosAcumulados", puntuacion);
        PlayerPrefs.Save(); 

        SceneManager.LoadScene(nextSceneName); // Cambiar a la escena especificada
    }
}