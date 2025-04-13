using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FondoInfinito : MonoBehaviour
{
    public float alturaFondo = 17.5f;
    public UnityEngine.Transform camara;


    private bool fondoInstanciado = false;

    void Update()
    {
        
        if (camara.position.y >= transform.position.y && !fondoInstanciado)
        {
            Vector3 nuevaPos = new Vector3(transform.position.x, transform.position.y + alturaFondo, transform.position.z);
            GameObject nuevoFondo = Instantiate(gameObject, nuevaPos, Quaternion.identity);
            nuevoFondo.GetComponent<FondoInfinito>().camara = camara;

            fondoInstanciado = true;
        }
        if (camara.position.y - transform.position.y > alturaFondo * 2)
        {
            Destroy(gameObject);
        }
    }
}

