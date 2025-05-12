using UnityEngine;

public class FondoBaseFijo : MonoBehaviour
{
    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void LateUpdate()
    {
        transform.position = posicionInicial;
    }
}