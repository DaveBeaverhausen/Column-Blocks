using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject bloquePrefab; // Prefab del bloque
    public Transform gancho; // Punto donde se crea el bloque (hijo del gancho)
    public Transform puntoCreacionBloque; // Transform vacío donde se instancia el bloque
    public GameObject gameOverPanel; // Panel de Game Over

    public Gancho ganchoScript;           // Referencia al script del gancho
    public float incrementoVelocidad = 0.5f;
    public float velocidadMaxima = 10f;


    private GameObject bloqueActual; // Referencia al bloque actual
    public CameraFollow cameraFollow;


    void Start()
    {
        gameOverPanel.SetActive(false); // Oculta el panel de Game Over al iniciar
        CrearNuevoBloque(); // Crea el primer bloque en el gancho
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bloqueActual != null)
        {
            SoltarBloque(); // Suelta el bloque actual al hacer clic
        }
    }

    void CrearNuevoBloque()
    {

        bloqueActual = Instantiate(bloquePrefab, puntoCreacionBloque.position, Quaternion.identity);
        bloqueActual.transform.parent = gancho; // Lo colgamos del gancho
        bloqueActual.GetComponent<Rigidbody2D>().simulated = false;
    }

    void SoltarBloque()
    {
        bloqueActual.transform.parent = null;
        Rigidbody2D rb = bloqueActual.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.linearVelocity = Vector2.zero;

        // Cámara: actualizar el último bloque
        if (cameraFollow != null)
        {
            cameraFollow.SetUltimoBloque(bloqueActual.transform);
        }

        // Aumentar velocidad del gancho
        if (ganchoScript != null)
        {
            ganchoScript.velocidad = Mathf.Min(ganchoScript.velocidad + incrementoVelocidad, velocidadMaxima);
        }

        bloqueActual = null;

        Invoke(nameof(CrearNuevoBloque), 0.5f);
    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
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
}
