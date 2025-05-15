

using UnityEngine;

public class Corte : MonoBehaviour
{
    public GameObject frutaPartidaPrefab; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Espada"))
        {
            
            Instantiate(frutaPartidaPrefab, transform.position, transform.rotation);

           
            Destroy(gameObject);
        }
    }
}