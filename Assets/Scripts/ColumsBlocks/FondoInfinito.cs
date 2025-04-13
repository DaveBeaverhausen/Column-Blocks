using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public float alturaFondo = 17.5f; // Altura de cada imagen del fondo
    public Transform camara; // Referencia a la cámara principal

    private Vector3 posicionInicial;
    private float limiteSuperior;
    private float limiteInferior;

    void Start()
    {
        // Guardar la posición inicial del fondo
        posicionInicial = transform.position;

        // Calcular límites superior e inferior basados en la altura del fondo
        limiteSuperior = posicionInicial.y + alturaFondo;
        limiteInferior = posicionInicial.y - alturaFondo;
    }

    void Update()
    {
        // Si la cámara supera el límite superior, reposicionar el fondo hacia abajo
        if (camara.position.y >= limiteSuperior)
        {
            ReposicionarFondo(-alturaFondo);
        }

        // Si la cámara supera el límite inferior, reposicionar el fondo hacia arriba
        if (camara.position.y <= limiteInferior)
        {
            ReposicionarFondo(alturaFondo);
        }
    }

    void ReposicionarFondo(float desplazamiento)
    {
        transform.position += new Vector3(0, desplazamiento, 0);

        // Actualizar límites después de reposicionar
        limiteSuperior += desplazamiento;
        limiteInferior += desplazamiento;
    }
}