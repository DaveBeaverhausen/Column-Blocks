using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float baseSpeed = 0.5f;     // Velocidad inicial
    public float maxSpeed = 5f;        // Velocidad máxima
    public float speedMultiplier = 1f; // Multiplicador de velocidad
    public float acceleration = 0.05f;  // Aceleración por segundo
    public float moveDistance = 20f;   // Rango de movimiento (x: -20 a +20)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Aumenta la velocidad gradualmente hasta el máximo
        speedMultiplier = Mathf.Min(speedMultiplier + acceleration * Time.deltaTime, maxSpeed / baseSpeed);

        // Movimiento lateral con velocidad progresiva y rango ampliado
        float newX = startPosition.x + Mathf.Sin(Time.time * (baseSpeed * speedMultiplier)) * moveDistance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // Método para aumentar la velocidad al recibir daño
    public void IncreaseSpeed(float amount)
    {
        speedMultiplier = Mathf.Min(speedMultiplier + amount, maxSpeed / baseSpeed);
    }
}