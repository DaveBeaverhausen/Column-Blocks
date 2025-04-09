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

    [Header("Control")]
    public float tiempoRestante = 90f; // 1:30
    private int puntuacion = 0;
    private bool juegoTerminado = false;

    public CameraFollow cameraFollow;
    public Gancho ganchoScript;
    public float incrementoVelocidad = 0.5f;
    public float velocidadMaxima = 10f;

    private GameObject bloqueActual;

    void Start()
    {
        gameOverPanel.SetActive(false);
        ActualizarUI();
        CrearNuevoBloque();
    }

    void Update()
    {
        if (juegoTerminado) return;

        // Input para soltar
        if (Input.GetMouseButtonDown(0) && bloqueActual != null)
        {
            SoltarBloque();
        }

        // Temporizador
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            GameOver();
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

        bloqueActual = null;

        Invoke(nameof(CrearNuevoBloque), 0.5f);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // pausa el juego
    }


    public void Reintentar()
    {
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

}
