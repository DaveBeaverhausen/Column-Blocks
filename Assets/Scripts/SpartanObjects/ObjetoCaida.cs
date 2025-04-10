using UnityEngine;

public class ObjetoCaida : MonoBehaviour
{
    public float velocidadBase = 2f;
    public float multiplicadorValocidad = 2f;
    public Marcador marcador;

    private bool contabilizado = false;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (velocidadBase *  multiplicadorValocidad) * Time.deltaTime);

        if (transform.position.y < -10f) Destroy(gameObject);
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
