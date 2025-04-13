using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public float alturaFondo = 17.5f; // Altura de cada imagen del fondo
    public Transform camara; // Referencia a la c�mara principal

    private Vector3 posicionInicial;
    private float limiteSuperior;
    private float limiteInferior;

    void Start()
    {
        // Guardar la posici�n inicial del fondo
        posicionInicial = transform.position;

        // Calcular l�mites superior e inferior basados en la altura del fondo
        limiteSuperior = posicionInicial.y + alturaFondo;
        limiteInferior = posicionInicial.y - alturaFondo;
    }

    void Update()
    {
        // Si la c�mara supera el l�mite superior, reposicionar el fondo hacia abajo
        if (camara.position.y >= limiteSuperior)
        {
            ReposicionarFondo(-alturaFondo);
        }

        // Si la c�mara supera el l�mite inferior, reposicionar el fondo hacia arriba
        if (camara.position.y <= limiteInferior)
        {
            ReposicionarFondo(alturaFondo);
        }
    }

    void ReposicionarFondo(float desplazamiento)
    {
        transform.position += new Vector3(0, desplazamiento, 0);

        // Actualizar l�mites despu�s de reposicionar
        limiteSuperior += desplazamiento;
        limiteInferior += desplazamiento;
    }
}