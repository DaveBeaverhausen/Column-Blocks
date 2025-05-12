using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ultimoBloque;
    public float velocidadMaxima = 4f; // Máxima velocidad de movimiento en Y
    public float verticalOffset = 6f;
    public float umbralAltura = 3f; // Altura mínima para activar movimiento

    private float minY;

    void Start()
    {
        minY = transform.position.y;
    }

    void LateUpdate()
    {
        if (ultimoBloque == null) return;

        float targetY = ultimoBloque.position.y + verticalOffset;

        // Solo mover si el bloque supera el umbral
        if (targetY > transform.position.y)
        {
            // Mover gradualmente hacia targetY con velocidad máxima
            float nuevaY = Mathf.MoveTowards(
                transform.position.y,
                targetY,
                velocidadMaxima * Time.deltaTime
            );

            transform.position = new Vector3(
                transform.position.x,
                nuevaY,
                transform.position.z
            );
        }
    }

    public void SetUltimoBloque(Transform bloque)
    {
        ultimoBloque = bloque;
    }
}