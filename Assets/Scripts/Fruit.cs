using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que toca la fruta tiene el tag "Controller", la fruta se destruye
        if (other.CompareTag("Controller"))
        {
            // Aquí podrías agregar efectos visuales y de sonido para el corte
            Destroy(gameObject);  // Destruir la fruta
        }
    }
}
