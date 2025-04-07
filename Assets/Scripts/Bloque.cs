using UnityEngine;

public class Bloque : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inclinacionMaxima = 30f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Si el bloque está demasiado inclinado, termina el juego
        if (Mathf.Abs(transform.rotation.z) > inclinacionMaxima)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("¡Game Over!");
        Time.timeScale = 0; // Pausa el juego
    }
}
