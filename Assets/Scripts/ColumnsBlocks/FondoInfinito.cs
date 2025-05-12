using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public float alturaFondo = 17.5f;
    public Transform camara;
    private bool fondoInstanciado = false;

    void Start()
    {
        // Asignar cámara automáticamente si no está asignada
        if (camara == null)
            camara = Camera.main.transform;
    }

    void Update()
    {
        if (camara == null) return; // Protección contra errores

        // Crear nuevo fondo ANTES de que la cámara llegue al borde
        float margenAnticipacion = 5f; // Crear el fondo 5 unidades antes
        if (camara.position.y + margenAnticipacion >= transform.position.y + alturaFondo && !fondoInstanciado)
        {
            // Crear clon en posición exacta para evitar huecos
            Vector3 nuevaPos = new Vector3(
                transform.position.x,
                transform.position.y + alturaFondo,
                transform.position.z
            );

            GameObject nuevoFondo = Instantiate(gameObject, nuevaPos, Quaternion.identity);

            // Asegurar que el clon tiene la referencia a la cámara
            FondoInfinito script = nuevoFondo.GetComponent<FondoInfinito>();
            script.camara = camara;
            script.fondoInstanciado = false; // Resetear para que el clon pueda crear su propio clon

            fondoInstanciado = true;
        }
    }
}