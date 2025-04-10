/*
**Usamos la clase Playerprefs que nos permite guardar y recuperar datos.
**De esta manera, podemos acumular la puntuación pantalla a pantalla.
*/
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class ScriptJuego : MonoBehaviour
{
    public GameObject[] objetosPrefab;
    public float tiempoTotal = 60f;
    public TextMeshProUGUI textoTiempo;
    public Marcador marcador;
    public string nextSceneName = "PreColumns"; 
    public GameObject gameOverPanel;
    public AudioClip sonidoGameOver;
    public TextMeshProUGUI textoMarca;

    private float velocidadCaida = 2f;
    private bool juegoActivo = true;
    private float limiteIzquierdo = -2.4f;
    private float limiteDerecho = 2.4f;
    private AudioSource audioSource;

    private void Start()
    {
        // Cargar los puntos acumulados al inicio (si los hay)
        if (marcador != null)
        {
            int puntosAcumulados = PlayerPrefs.GetInt("PuntosAcumulados", 0); // 0 es el valor por defecto si no existe
            marcador.EstablecerPuntuacion(puntosAcumulados);
        }

        StartCoroutine(GenerarObjetos());
        gameOverPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!juegoActivo) return;

        tiempoTotal -= Time.deltaTime;
        velocidadCaida += Time.deltaTime * 0.1f;

        if (tiempoTotal <= 0)
        {
            tiempoTotal = 0;
            juegoActivo = false;

            // Obtener puntuación y guardar
            int puntosActuales = marcador.ObtenerPuntuacion();
            PlayerPrefs.SetInt("Puntos_Prueba2", puntosActuales);
            PlayerPrefs.Save();

            if (textoMarca != null)
            {
                textoMarca.text = "Marca: " + puntosActuales;
            }

            gameOverPanel.SetActive(true);

            if (sonidoGameOver != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoGameOver);
            }

        }

        MostrarTiempo(tiempoTotal);
    }

    IEnumerator GenerarObjetos()
    {
        float intervaloInicial = 2f; // Intervalo inicial entre objetos
        float intervaloMinimo = 0.5f; // Intervalo mínimo entre objetos
        float reduccionIntervalo = 0.1f; // Reducción del intervalo por cada objeto generado

        while (juegoActivo)
        {
            int indice = Random.Range(0, objetosPrefab.Length);

            // Generar posición X dentro de los límites establecidos
            float posicionX = Random.Range(limiteIzquierdo, limiteDerecho);
            Vector3 posicion = new Vector3(posicionX, 10f, 0f);

            GameObject nuevoObjeto = Instantiate(objetosPrefab[indice], posicion, Quaternion.identity);
            ObjetoCaida script = nuevoObjeto.AddComponent<ObjetoCaida>();

            script.velocidadBase = velocidadCaida;
            script.marcador = marcador;

            // Esperar antes de generar el siguiente objeto
            yield return new WaitForSeconds(intervaloInicial);

            // Reducir gradualmente el intervalo hasta alcanzar el mínimo
            intervaloInicial = Mathf.Max(intervaloInicial - reduccionIntervalo, intervaloMinimo);
        }
    }

    void MostrarTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        textoTiempo.text = $"Time {minutos:00}:{segundos:00}";
    }

    public void IrASiguientePantalla()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}