using UnityEngine;

public class Bloque : MonoBehaviour
{
    public GameController gameController;

    private bool haColisionado = false;
    private Rigidbody2D rb;
    private float inclinacionMaxima = 30f;
    private float posicionMinimaY = -6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float rotZ = transform.eulerAngles.z;
        float inclinacion = Mathf.Abs(NormalizarAngulo(rotZ));

        if (inclinacion > inclinacionMaxima || transform.position.y < posicionMinimaY)
        {
            if (gameController != null)
                gameController.GameOver();
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

            if (gameController != null)
            {
                gameController.SumarPunto(); 
            }
        }
    }

}
