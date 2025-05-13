using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            // Proyectil esquivado (pas� sin golpear el escudo)
            GameManager.Instance.AddPoint();
            Destroy(other.gameObject);
        }
    }
}