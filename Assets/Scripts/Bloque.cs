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
<<<<<<< HEAD
        // Si el bloque está demasiado inclinado, termina el juego
        if (Mathf.Abs(transform.rotation.z) > inclinacionMaxima)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("¡Game Over! Torre colapsada.");
        Time.timeScale = 0; // Pausa el juego
    }
=======
        float rotZ = transform.eulerAngles.z;
        float inclinacion = Mathf.Abs(NormalizarAngulo(rotZ));

        if (inclinacion > inclinacionMaxima)
        {
            FindObjectOfType<GameController>().GameOver();
        }
    }

    float NormalizarAngulo(float angulo)
    {
        // Convierte 0–360 a -180 a 180
        if (angulo > 180)
            angulo -= 360;
        return angulo;
    }

>>>>>>> 6a470cc6612f9c8f0c168dd99da820ef7b1a3ae0
}
