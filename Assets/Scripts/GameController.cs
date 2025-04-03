using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject bloquePrefab;
    public Transform posicionGancho;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta tap o clic
        {
            SoltarBloque();
        }
    }

    void SoltarBloque()
    {
        Instantiate(bloquePrefab, posicionGancho.position, Quaternion.identity);
    }
}