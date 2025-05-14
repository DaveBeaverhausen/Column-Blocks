using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public SpawnManager spawnManager;
    private Transform shieldTransform;
    private bool isLaunched = false;
    private Vector3 direction;
    private bool hasCollided = false;

    public void Initialize(SpawnManager manager, Transform shield)
    {
        spawnManager = manager;
        shieldTransform = shield;
        gameObject.SetActive(true); // Activa el proyectil cuando está listo
    }

    void Update()
    {
        if (!isLaunched)
        {
            HandleTouchInput();
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                LaunchProjectile();
            }
        }
    }

    void LaunchProjectile()
    {
        if (shieldTransform == null) return;

        // Calcula dirección hacia la posición ACTUAL del escudo
        direction = (shieldTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        isLaunched = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasCollided) return; // Evita múltiples colisiones

        if (other.CompareTag("Shield"))
        {
            GameManager.Instance.AddPoint();
            DestroyProjectile();
        }
        else if (other.CompareTag("ScoreZone"))
        {
            GameManager.Instance.TakeDamage();
            DestroyProjectile();
        }

        hasCollided = true; // Marca como colisionado
    }

    void DestroyProjectile()
    {
        spawnManager?.ProjectileDestroyed();
        Destroy(gameObject);
    }
}