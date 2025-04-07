using UnityEngine;

public class ControllerSpartan : MonoBehaviour
{
    Vector3 direccion;
    public int velocidad = 10;
    private float limiteIzquierdo = -2.4f;
    private float limiteDerecho = 2.4f;

    void Update()
    {
        // Movimiento con teclado (para pruebas en PC)
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            direccion = horizontal > 0 ? Vector3.right : Vector3.left;
            MoverJugador();
        }

        // Movimiento táctil para móvil
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.position.x > Screen.width / 2)
                direccion = Vector3.right;
            else
                direccion = Vector3.left;

            MoverJugador();
        }
    }

    void MoverJugador()
    {
        Vector3 nuevaPosicion = transform.position + direccion * velocidad * Time.deltaTime;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);
        transform.position = nuevaPosicion;
    }
}
