using UnityEngine;

public class Gancho : MonoBehaviour
{
    public float velocidadInicial = 3.5f;
    public float incrementoVelocidad = 0.1f; // Incremento adicional para ajustar la velocidad
    private float velocidadActual; // Velocidad actual del gancho
    private float limiteIzq;
    private float limiteDer;
    private bool moviendoDerecha = true;

    void Start()
    {
        velocidadActual = velocidadInicial; // Inicializar con la velocidad base
        CalcularLimitesPantalla();
    }

    void CalcularLimitesPantalla()
    {
        Camera mainCamera = Camera.main;

        // Convertir las coordenadas de la cámara a coordenadas del mundo
        Vector3 esquinaIzquierda = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        Vector3 esquinaDerecha = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));

        limiteIzq = esquinaIzquierda.x;
        limiteDer = esquinaDerecha.x;

        Debug.Log($"Límites calculados: Izquierdo={limiteIzq}, Derecho={limiteDer}");
    }

    void Update()
    {
        // Incrementar la velocidad si es necesario (puedes ajustar este valor según el tiempo o eventos)
        velocidadActual += incrementoVelocidad * Time.deltaTime;

        // Mueve el gancho de lado a lado
        float movimiento = velocidadActual * Time.deltaTime;
        transform.position += new Vector3(moviendoDerecha ? movimiento : -movimiento, 0, 0);

        // Cambia de dirección al llegar a los límites
        if (transform.position.x >= limiteDer) moviendoDerecha = false;
        if (transform.position.x <= limiteIzq) moviendoDerecha = true;
    }
}