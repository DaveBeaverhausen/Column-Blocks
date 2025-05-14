using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float baseSpeed = 0.5f;     // Velocidad inicial
    public float maxSpeed = 5f;        // Velocidad máxima
    public float speedMultiplier = 0.3f; // Multiplicador de velocidad
    public float acceleration = 0.05f;  // Aceleración por segundo
    public float moveDistance = 20f;   // Rango de movimiento (x: -20 a +20)

    private Vector3 startPosition;
    private float currentSpeed;
    private float speedProgress;

    void Start()
    {
        startPosition = transform.position;
        currentSpeed = baseSpeed;
        speedProgress = 0f;
    }

    void Update()
    {
        // Aumento progresivo de la velocidad
        speedProgress = Mathf.Clamp01(speedProgress + acceleration * Time.deltaTime);
        currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, speedProgress);

        // Movimiento suavizado
        float phase = Time.time * currentSpeed;
        float newX = startPosition.x + Mathf.Sin(phase) * moveDistance;

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}