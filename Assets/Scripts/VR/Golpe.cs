using UnityEngine;

public class Golpe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Espada"))
        {
            

            VrScript vrManager = FindObjectOfType<VrScript>();
            if (vrManager != null)
            {
                vrManager.RecibirGolpe(); 
            }

            Destroy(gameObject); 
        }
    }
}
