using UnityEngine;

public class ObjetoCaida : MonoBehaviour
{
    public float velocidadBase = 2f;
    public float multiplicadorValocidad = 2f;
    public Marcador marcador;

    public ScriptJuego scriptJuego;
    private bool contabilizado = false;
    private float limiteBordeInferior;

    void Start()
    {
        
        scriptJuego = FindFirstObjectByType<ScriptJuego>();

        
        Vector3 puntoInferior = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.1f, 0));
        limiteBordeInferior = puntoInferior.y;
    }

    void Update()
    {
        
        if (scriptJuego == null || scriptJuego.juegoActivo)
        {
            transform.Translate(Vector3.down * (velocidadBase * multiplicadorValocidad) * Time.deltaTime);
        }

        if (transform.position.y < limiteBordeInferior) Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("ZonaEsquivar") && !contabilizado)
        {
            marcador.IncrementarPuntuacion(1); 
            contabilizado = true;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !contabilizado)
        {
            contabilizado = true;

            // Llamar a PerderVida para reducir las vidas del jugador
            if (scriptJuego != null)
            {
                scriptJuego.PerderVida();
            }

            AudioSource audio = collision.gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
            Destroy(gameObject);
        }
    }

}