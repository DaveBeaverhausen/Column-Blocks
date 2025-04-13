using UnityEngine;

public class Bloque : MonoBehaviour
{
    public GameController gameController;

    private bool haColisionado = false;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private float inclinacionMaxima = 30f;
    private float posicionMinimaY = -6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float rotZ = transform.eulerAngles.z;
        float inclinacion = Mathf.Abs(NormalizarAngulo(rotZ));

        if (inclinacion > inclinacionMaxima || transform.position.y < posicionMinimaY)
        {
            if (gameController != null)
            {
                gameController.GameOver();
                gameController.BloqueAsentado(); // Liberar siguiente intento aunque sea Game Over
            }
        }
    }

    float NormalizarAngulo(float angulo)
    {
        return angulo > 180 ? angulo - 360 : angulo;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!haColisionado && (collision.collider.CompareTag("Plataforma") || collision.collider.CompareTag("Bloque")))
        {
            haColisionado = true;

            if (audioSource != null) 
            { 
                // Reproducir el sonido de colisión
                audioSource.Play();
            }

            if (gameController != null)
            {
                gameController.SumarPunto();
                gameController.BloqueAsentado(); // solo ahora permitimos lanzar otro
            }
        }
    }
}
