using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float sensitivity = 0.25f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Movimiento por acelerómetro
        Vector3 movement = new Vector3(Input.acceleration.x, 0, Input.acceleration.y) * sensitivity;
        transform.position = initialPosition + movement;

        // Alternativa para control táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            transform.position = new Vector3(touchPosition.x, initialPosition.y, initialPosition.z);
        }
    }
}
