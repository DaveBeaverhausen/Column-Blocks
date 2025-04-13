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
        // Calcular límites de pantalla para la generación de objetos
        CalcularLimitesPantalla();

        // Cargar los puntos acumulados al inicio (si los hay)
        if (marcador != null)
        {
            marcador.EstablecerPuntuacion(0);
        }

        StartCoroutine(GenerarObjetos());
        gameOverPanel.SetActive(false);
    }

    void CalcularLimitesPantalla()
    {
        // Convertir los puntos del viewport a coordenadas del mundo
        Vector3 esquinaIzquierda = Camera.main.ViewportToWorldPoint(new Vector3(margenPantalla, 0.5f, 0));
        Vector3 esquinaDerecha = Camera.main.ViewportToWorldPoint(new Vector3(1 - margenPantalla, 0.5f, 0));

        limiteIzquierdo = esquinaIzquierda.x;
        limiteDerecho = esquinaDerecha.x;

        // Calcular la altura de generación basada en el viewport superior
        Vector3 puntoSuperior = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0));
        alturaGeneracion = puntoSuperior.y;

        Debug.Log($"Límites calculados: Izquierdo={limiteIzquierdo}, Derecho={limiteDerecho}, Altura={alturaGeneracion}");
    }

    private void Update()
    {
        if (!juegoActivo) return;

        tiempoTotal -= Time.deltaTime;
        MostrarTiempo(tiempoTotal);
        velocidadCaida += Time.deltaTime * 0.1f;
        
        if (tiempoTotal <= 0)
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
            Vector3 posicion = new Vector3(posicionX, alturaGeneracion, 0f);

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
        tiempo = Mathf.Max(tiempo, 0); // Prevenir números negativos

        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);

        // Cambiar el color si el tiempo es crítico
        if (tiempo <= 10f)
        {
            textoTiempo.color = Color.red; // o usa un Color personalizado si querés
        }
        else
        {
            textoTiempo.color = Color.black; // o tu color original
        }

        textoTiempo.text = $"TIEMPO\n{minutos:00}:{segundos:00}";
    }


    public void IrASiguientePantalla()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}