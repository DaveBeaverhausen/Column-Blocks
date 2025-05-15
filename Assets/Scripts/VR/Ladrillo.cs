using UnityEngine;

public class Ladrillo : MonoBehaviour
{
    public GameObject ladrilloPrefab;
    public float spawnInterval = 4f;
    public float xRange = 5f;
    public float zRange = 5f;
    public float fallSpeed = 0.5f;

    void Start()
    {
        InvokeRepeating(nameof(SoltarLadrillo), 3f, spawnInterval);
    }

    void SoltarLadrillo()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-xRange, xRange),
            transform.position.y,
            Random.Range(-zRange, zRange)
        );

        GameObject ladrillo = Instantiate(ladrilloPrefab, spawnPos, Quaternion.identity);
        Rigidbody rb = ladrillo.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector3(0, -fallSpeed, 0);
        }
    }
}
