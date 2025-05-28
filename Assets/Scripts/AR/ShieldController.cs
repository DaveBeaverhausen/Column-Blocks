using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float baseSpeed = 0.5f;     // Velocidad inicial
    public float maxSpeed = 5f;        // Velocidad m�xima 
    public float acceleration = 0.02f;  // Aceleraci�n por segundo
    public float moveDistance = 20f;    // Rango de movimiento

    private Vector3 startPosition;
    private float currentSpeed;
    private float speedProgress;
    private float oscillationTimer = 0f; // Temporizador independiente para oscilaci�n

    void Start()
    {
        startPosition = transform.position;
        currentSpeed = baseSpeed;
        speedProgress = 0f;
    }

    void Update()
    {
        // Incrementar velocidad gradualmente (hasta 20 segundos para llegar al m�ximo)
        if (speedProgress < 1f)
        {
            speedProgress = Mathf.Clamp01(speedProgress + acceleration * Time.deltaTime);
            currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, speedProgress);
        }

        // Usar un temporizador independiente para la oscilaci�n
        oscillationTimer += currentSpeed * Time.deltaTime;

        // Mantener el timer en un rango manejable
        if (oscillationTimer > 100f)
            oscillationTimer -= 100f;

        // Movimiento suavizado con frecuencia constante
        float newX = startPosition.x + Mathf.Sin(oscillationTimer) * moveDistance;

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
