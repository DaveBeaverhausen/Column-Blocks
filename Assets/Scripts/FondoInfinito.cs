using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public float alturaFondo = 17f;
    public Transform camara;

    private bool fondoInstanciado = false;

    void Update()
    {
        // Si la cámara está a punto de llegar a este fondo
        if (camara.position.y >= transform.position.y && !fondoInstanciado)
        {
            Vector3 nuevaPos = new Vector3(transform.position.x, transform.position.y + alturaFondo, transform.position.z);
            GameObject nuevoFondo = Instantiate(gameObject, nuevaPos, Quaternion.identity);
            nuevoFondo.GetComponent<FondoInfinito>().camara = camara;

            fondoInstanciado = true;
        }
    }
}
