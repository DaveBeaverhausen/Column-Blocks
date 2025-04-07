using UnityEngine;

public class Gancho : MonoBehaviour
{
     public float velocidad = 4f;
    private float limiteIzq = -4.5f;
    private float limiteDer = 4.5f;
    private bool moviendoDerecha = true;
        
    void Update()
    {
        // Mueve el gancho de lado a lado
        float movimiento = velocidad * Time.deltaTime;
        transform.position += new Vector3(moviendoDerecha ? movimiento : -movimiento, 0, 0);

        // Cambia de dirección al llegar a los límites
        if (transform.position.x >= limiteDer) moviendoDerecha = false;
        if (transform.position.x <= limiteIzq) moviendoDerecha = true;
        
    }
}   

