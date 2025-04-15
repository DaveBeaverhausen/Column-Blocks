using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoEscalarFondoDinamico : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector2 ultimaResolucion;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        AjustarEscala();
        ultimaResolucion = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // Si la resoluci�n cambia (por rotaci�n u otro motivo)
        if (Screen.width != ultimaResolucion.x || Screen.height != ultimaResolucion.y)
        {
            AjustarEscala();
            ultimaResolucion = new Vector2(Screen.width, Screen.height);
        }
    }

    void AjustarEscala()
    {
        if (sr == null || sr.sprite == null || Camera.main == null) return;

        float alturaCamara = Camera.main.orthographicSize * 2f;
        float anchoCamara = alturaCamara * Screen.width / Screen.height;

        Vector2 tama�oSprite = sr.sprite.bounds.size;

        Vector3 escala = transform.localScale;
        escala.x = anchoCamara / tama�oSprite.x;
        escala.y = alturaCamara / tama�oSprite.y;
        transform.localScale = escala;
    }
}
