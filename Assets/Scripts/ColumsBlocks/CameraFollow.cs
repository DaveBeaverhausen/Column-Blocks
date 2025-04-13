using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ultimoBloque;  
    public float smoothSpeed = 2f;
    public float verticalOffset = 6f; 

    private float minY;

    void Start()
    {
        minY = transform.position.y;
    }

    void LateUpdate()
    {
        if (ultimoBloque == null) return;

        float targetY = ultimoBloque.position.y + verticalOffset;

        if (targetY > transform.position.y)
        {
            Vector3 destino = new Vector3(transform.position.x, targetY, transform.position.z);
            Vector3 suavizado = Vector3.Lerp(transform.position, destino, smoothSpeed * Time.deltaTime);
            transform.position = suavizado;

            minY = suavizado.y;
        }
    }

    public void SetUltimoBloque(Transform bloque)
    {
        ultimoBloque = bloque;
    }
}
