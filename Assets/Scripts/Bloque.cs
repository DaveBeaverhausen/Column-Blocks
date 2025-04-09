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

}
