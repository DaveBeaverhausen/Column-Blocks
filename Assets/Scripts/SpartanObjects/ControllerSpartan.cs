using UnityEngine;

public class ControllerSpartan : MonoBehaviour
{
    Vector3 direccion;
    public int velocidad = 10;
    private float limiteIzquierdo;
    private float limiteDerecho;
    private ScriptJuego scriptJuego;

    
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

        // Movimiento con teclado (para pruebas en PC)
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            direccion = horizontal > 0 ? Vector3.right : Vector3.left;
            MoverJugador();
        }

        // Movimiento tactil para movil
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.position.x > Screen.width / 2)
                direccion = Vector3.right;
            else
                direccion = Vector3.left;

            MoverJugador();
        }
    }

    void MoverJugador()
    {
        Vector3 nuevaPosicion = transform.position + direccion * velocidad * Time.deltaTime;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);
        transform.position = nuevaPosicion;
    }
}