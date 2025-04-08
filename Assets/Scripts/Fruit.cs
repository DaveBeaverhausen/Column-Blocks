using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona con la fruta es un controlador de VR
        if (other.CompareTag("Controller"))
        {
            // Aquí podrías agregar efectos visuales o de sonido para el corte
            Destroy(gameObject);  // Destruir la fruta al cortarla
        }
    }
}
