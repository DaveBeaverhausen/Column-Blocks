using UnityEngine;

public class ControllerSpartan : MonoBehaviour
{
    Vector3 direccion;
    public float velocidad = 10;
    private float limiteIzquierdo;
    private float limiteDerecho;
    private ScriptJuego scriptJuego;

    // Nuevas variables para la ralentización
    private float velocidadActual = 0f;
    public float factorAceleracion = 5f;
    public float factorDesaceleracion = 3f;
    private bool recibiendoEntrada = false;

    [Range(0, 0.2f)]
    public float margenPantalla = 0.05f;

    void Start()
    {
        CalcularLimitesPantalla();
        scriptJuego = FindFirstObjectByType<ScriptJuego>();
    }

    
    void OnRectTransformDimensionsChange()
    {
        CalcularLimitesPantalla();
    }

    void CalcularLimitesPantalla()
    {
        
        Vector3 esquinaIzquierda = Camera.main.ViewportToWorldPoint(new Vector3(margenPantalla, 0.5f, 0));
        Vector3 esquinaDerecha = Camera.main.ViewportToWorldPoint(new Vector3(1 - margenPantalla, 0.5f, 0));

        limiteIzquierdo = esquinaIzquierda.x;
        limiteDerecho = esquinaDerecha.x;

        Debug.Log($"Limites calculados: Izquierdo={limiteIzquierdo}, Derecho={limiteDerecho}");
    }

    void Update()
    {
        // No mover espartano si el juego esta inactivo
        if (scriptJuego == null || !scriptJuego.juegoActivo) return;

        recibiendoEntrada = false;

        // Movimiento con teclado (para pruebas en PC)
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            direccion = horizontal > 0 ? Vector3.right : Vector3.left;

            // Acelerar gradualmente
            velocidadActual = Mathf.MoveTowards(velocidadActual, velocidad, factorAceleracion * Time.deltaTime);
        }

        // Movimiento tactil para movil
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.position.x > Screen.width / 2)
                direccion = Vector3.right;
            else
                direccion = Vector3.left;

            recibiendoEntrada = true;

            // Acelerar gradualmente
            velocidadActual = Mathf.MoveTowards(velocidadActual, velocidad, factorAceleracion * Time.deltaTime);            
        }

        // Ralentizar si no hay entrada
        if (!recibiendoEntrada && velocidadActual > 0)
        {
            velocidadActual = Mathf.MoveTowards(velocidadActual, 0, factorDesaceleracion * Time.deltaTime);
        }

        // Mover jugador si tiene velocidad
        if (velocidadActual > 0)
        {
            MoverJugador();
        }
    }

    void MoverJugador()
    {
        Vector3 nuevaPosicion = transform.position + direccion * velocidadActual * Time.deltaTime;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);
        transform.position = nuevaPosicion;
    }
}