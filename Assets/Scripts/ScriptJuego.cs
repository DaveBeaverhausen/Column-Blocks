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

    private float velocidadCaida = 2f;
    private bool juegoActivo = true;
    private float limiteIzquierdo = -2.4f;
    private float limiteDerecho = 2.4f;

    private void Start()
    {
        StartCoroutine(GenerarObjetos());
    }

    private void Update()
    {
        if (!juegoActivo) return;

        tiempoTotal -= Time.deltaTime;
        velocidadCaida += Time.deltaTime * 0.01f;

        if (tiempoTotal <= 0)
        {
            tiempoTotal = 0;
            juegoActivo = false;
            Debug.Log("Juego terminado");
            SceneManager.LoadScene("NextGame");
        }

        MostrarTiempo(tiempoTotal);
    }

 IEnumerator GenerarObjetos()
{
    float intervaloInicial = 2f; // Intervalo inicial entre objetos
    float intervaloMinimo = 0.5f; // Intervalo mínimo entre objetos
    float reduccionIntervalo = 0.01f; // Reducción del intervalo por cada objeto generado

    while (juegoActivo)
    {
        int indice = Random.Range(0, objetosPrefab.Length);

        // Generar posición X dentro de los límites establecidos
        float posicionX = Random.Range(limiteIzquierdo, limiteDerecho);
        Vector3 posicion = new Vector3(posicionX, 10f, 0f);

        GameObject nuevoObjeto = Instantiate(objetosPrefab[indice], posicion, Quaternion.identity);

        // Agregar Rigidbody a la fruta para que sea afectada por la física
        Rigidbody rb = nuevoObjeto.AddComponent<Rigidbody>();
        rb.useGravity = true;  // Asegurarse de que la gravedad afecte la fruta.

        // Si la fruta tiene un Collider (es necesario para detectar los cortes), lo mantenemos.
        // Asegúrate de que el prefab tenga un Collider adecuado (como un BoxCollider o SphereCollider).
        
        // Ajustamos la velocidad de caída
        ObjetoCaida script = nuevoObjeto.AddComponent<ObjetoCaida>();
        script.velocidadBase = velocidadCaida;  // La velocidad base de caída de la fruta
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
        textoTiempo.text = $"{minutos:00}:{segundos:00}";
    }
}