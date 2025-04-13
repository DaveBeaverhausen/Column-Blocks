using UnityEngine;

public class ObjetoCaida : MonoBehaviour
{
    public float velocidadBase = 2f;
    public float multiplicadorValocidad = 2f;
    public Marcador marcador;

    private ScriptJuego scriptJuego;
    private bool contabilizado = false;
    private float limiteBordeInferior;

    void Start()
    {
        // Buscar la referencia al controlador del juego
        scriptJuego = FindFirstObjectByType<ScriptJuego>();

        // Calcular el límite inferior basado en la cámara
        Vector3 puntoInferior = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.1f, 0));
        limiteBordeInferior = puntoInferior.y;
    }

    void Update()
    {
        // Solo mover si el juego está activo
        if (scriptJuego == null || scriptJuego.juegoActivo)
        {
            transform.Translate(Vector3.down * (velocidadBase * multiplicadorValocidad) * Time.deltaTime);
        }

        if (transform.position.y < limiteBordeInferior) Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Si el objeto sale de la "zona de esquivar" sin colisionar
        if (other.CompareTag("ZonaEsquivar") && !contabilizado)
        {
            marcador.IncrementarPuntuacion(1); // Suma 1 punto
            contabilizado = true;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con el jugador
        if (collision.gameObject.CompareTag("Player") && !contabilizado)
        {
            marcador.DecrementarPuntuacion(1); // Resta 1 punto
            contabilizado = true;
            AudioSource audio = collision.gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
            Destroy(gameObject);
        }
    }
}

/*
**Usamos la clase Playerprefs que nos permite guardar y recuperar datos.
**De esta manera, podemos acumular la puntuación pantalla a pantalla.
*/