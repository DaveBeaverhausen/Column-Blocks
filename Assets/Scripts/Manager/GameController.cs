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
    public bool juegoTerminado = false;
    private bool bloqueEsperandoAsentarse = false; 

    [Header("Transición al finalizar el tiempo")]
    public string nextSceneName = "ErrorVR"; // Nombre de la escena a la que cambiar cuando se acabe el tiempo

    public CameraFollow cameraFollow;
    public Gancho ganchoScript;
    public float incrementoVelocidad = 0.5f;
    public float velocidadMaxima = 10f;
    private GameObject bloqueActual;

    public int vidasIniciales = 5;
    private int vidasActuales;
    public TextMeshProUGUI Vidas;

    void Start()
    {
        // Cargar los puntos acumulados al inicio

        puntuacion = 0;

        gameOverPanel.SetActive(false);
        vidasActuales = vidasIniciales;
        ActualizarUI();
        CrearNuevoBloque();
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
            GameOver();
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntuacion != null)
            textoPuntuacion.text = "MARCA\n" + puntuacion;

        if (textoTiempo != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
            textoTiempo.text = $"TIEMPO\n{minutos:00}:{segundos:00}";

            //Cambiar color si quedan menos de 10 segundos
            if (tiempoRestante <= 10f)
            {
                textoTiempo.color = Color.red;
            }
            else
            {
                textoTiempo.color = Color.black; 
            }
        }

        if (Vidas != null)
            Vidas.text = "VIDAS: " + vidasActuales;
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
        Invoke(nameof(CrearNuevoBloque), 0.25f);
    }

    public void GameOver()
    {
        if (juegoTerminado) return; // Bloquea llamadas duplicadas

        juegoTerminado = true;

        PlayerPrefs.SetInt("Puntos_Prueba2", puntuacion); // Guardar puntuación de esta prueba
        PlayerPrefs.Save();

        if (textoMarca != null)
        {
            textoMarca.text = "Marca: " + puntuacion;
        }

        gameOverPanel.SetActive(true);
        AudioManager.Instance.ReproducirSonidoGameOver();
        Time.timeScale = 0f;
        
        StartCoroutine(CargarEscenaFinal());
    }

    IEnumerator CargarEscenaFinal()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de cambiar
        SceneManager.LoadScene("GameOverPanel");
    }

    public void SumarPunto()
    {
        puntuacion++;
        ActualizarUI();
    }

    public void IrASiguienteEscena()
    {
        Time.timeScale = 1;

        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("SceneLoader.Instance no encontrado.");
        }
    }

}