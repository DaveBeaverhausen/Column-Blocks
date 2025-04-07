using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject bloquePrefab; // Prefab del bloque
    public Transform posicionGancho; // Punto donde se crea el bloque (hijo del gancho)
    public GameObject gameOverPanel; // Panel de Game Over

    private GameObject bloqueActual; // Referencia al bloque actual

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
        bloqueActual = Instantiate(bloquePrefab, posicionGancho.position, Quaternion.identity);
        bloqueActual.transform.parent = posicionGancho; // Lo hacemos hijo del gancho para que lo siga
        bloqueActual.GetComponent<Rigidbody2D>().simulated = false; // Desactiva física para que no caiga aún
    }

    void SoltarBloque()
    {
        bloqueActual.transform.parent = null; // Lo soltamos del gancho
        bloqueActual.GetComponent<Rigidbody2D>().simulated = true; // Activamos física para que caiga
        bloqueActual = null;

        // Creamos otro bloque luego de una pequeña pausa
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
