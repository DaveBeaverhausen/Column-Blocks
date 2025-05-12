using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class ScriptJuego : MonoBehaviour
{
    public GameObject[] objetosPrefab;
    public float tiempoTotal = 90f;
    public TextMeshProUGUI textoTiempo;
    public Marcador marcador;
    public string nextSceneName = "PreColumns";
    public GameObject gameOverPanel;
    public TextMeshProUGUI textoMarca;

    public int vidasIniciales = 5;
    private int vidasActuales;
    public TextMeshProUGUI Vidas;

    private float velocidadCaida = 3f;
    public bool juegoActivo = true;
    private float limiteIzquierdo;
    private float limiteDerecho;

    // Altura desde donde aparecen los objetos
    private float alturaGeneracion = 10f;

    // Margen para los bordes
    [Range(0, 0.2f)]
    public float margenPantalla = 0.05f;

    private void Start()
    {
        CalcularLimitesPantalla();

        if (marcador != null)
            marcador.EstablecerPuntuacion(0);

        vidasActuales = vidasIniciales;
        ActualizarVidas();

        StartCoroutine(GenerarObjetos());
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (!juegoActivo) return;

        tiempoTotal -= Time.deltaTime;
        MostrarTiempo(tiempoTotal);
        velocidadCaida += Time.deltaTime * 0.1f;

        // Fin de juego por vidas agotadas
        if (vidasActuales <= 0)
        {
            FinalizarJuego();
            return;
        }

        // Fin de juego por tiempo agotado
        if (tiempoTotal <= 0)
        {
            FinalizarJuego();
        }
    }

    public void PerderVida()
    {
        if (!juegoActivo) return;

        vidasActuales--;
        ActualizarVidas();

        // Comprobar si se agotaron las vidas
        if (vidasActuales <= 0)
        {
            tiempoTotal = 0;
            juegoActivo = false;

            // Obtener puntuación y guardar
            int puntosActuales = marcador.ObtenerPuntuacion();
            PlayerPrefs.SetInt("Puntos_Prueba1", puntosActuales);
            PlayerPrefs.Save();

            if (textoMarca != null)
            {
                textoMarca.text = "Marca: " + puntosActuales;
            }

            gameOverPanel.SetActive(true);
            AudioManager.Instance.ReproducirSonidoGameOver();
        }
    }

    private void ActualizarVidas()
    {
        if (Vidas != null)
        {
            Vidas.text = "VIDAS: " + vidasActuales;
        }
    }

    private void FinalizarJuego()
    {
        tiempoTotal = 0;
        juegoActivo = false;

        int puntosActuales = marcador.ObtenerPuntuacion();
        PlayerPrefs.SetInt("Puntos_Prueba1", puntosActuales);
        PlayerPrefs.Save();

        if (textoMarca != null)
            textoMarca.text = "Marca: " + puntosActuales;

        gameOverPanel.SetActive(true);
        AudioManager.Instance.ReproducirSonidoGameOver();
    }

    void CalcularLimitesPantalla()
    {
        Vector3 esquinaIzquierda = Camera.main.ViewportToWorldPoint(new Vector3(margenPantalla, 0.5f, 0));
        Vector3 esquinaDerecha = Camera.main.ViewportToWorldPoint(new Vector3(1 - margenPantalla, 0.5f, 0));

        limiteIzquierdo = esquinaIzquierda.x;
        limiteDerecho = esquinaDerecha.x;

        Vector3 puntoSuperior = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0));
        alturaGeneracion = puntoSuperior.y;
    }

    IEnumerator GenerarObjetos()
    {
        float intervaloInicial = 2f;
        float intervaloMinimo = 0.5f;
        float reduccionIntervalo = 0.1f;

        while (juegoActivo)
        {
            int indice = Random.Range(0, objetosPrefab.Length);
            float posicionX = Random.Range(limiteIzquierdo, limiteDerecho);
            Vector3 posicion = new Vector3(posicionX, alturaGeneracion, 0f);

            GameObject nuevoObjeto = Instantiate(objetosPrefab[indice], posicion, Quaternion.identity);
            ObjetoCaida script = nuevoObjeto.AddComponent<ObjetoCaida>();
            script.velocidadBase = velocidadCaida;
            script.marcador = marcador;
            script.scriptJuego = this;

            yield return new WaitForSeconds(intervaloInicial);
            intervaloInicial = Mathf.Max(intervaloInicial - reduccionIntervalo, intervaloMinimo);
        }
    }

    void MostrarTiempo(float tiempo)
    {
        tiempo = Mathf.Max(tiempo, 0);

        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);

        if (tiempo <= 10f)
            textoTiempo.color = Color.red;
        else
            textoTiempo.color = Color.black;

        textoTiempo.text = $"TIEMPO\n{minutos:00}:{segundos:00}";
    }

    public void IrASiguientePantalla()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}