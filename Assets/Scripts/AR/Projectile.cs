using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public SpawnManager spawnManager;

    private bool isLaunched = false;
    private Vector3 direction;

    void Update()
    {
        // Si el proyectil ya fue lanzado, moverlo
        if (isLaunched)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        // Si no está lanzado, verificar interacción
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Comprobar si el toque comenzó en este frame (para lanzar solo una vez)
            if (touch.phase == TouchPhase.Began)
            {
                // Raycast para ver si tocó este proyectil
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    LaunchProjectile();
                }
            }
        }
    }

    public void LaunchProjectile()
    {
        // Calcula dirección hacia el escudo
        Vector3 targetPos = GameObject.FindGameObjectWithTag("Shield").transform.position;
        direction = (targetPos - transform.position).normalized;

        // Orienta el proyectil hacia el objetivo
        transform.rotation = Quaternion.LookRotation(direction);

        // Marca como lanzado
        isLaunched = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            GameManager.Instance.AddPoint();
            DestroyProjectile();
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        if (spawnManager != null)
            spawnManager.ProjectileDestroyed();

        Destroy(gameObject);
    }
}