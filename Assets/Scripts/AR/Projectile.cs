using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield")) Destroy(gameObject);
        if (other.CompareTag("Player")) GameManager.Instance.TakeDamage();
    }
}