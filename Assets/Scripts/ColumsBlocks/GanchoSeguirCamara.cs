using UnityEngine;

public class GanchoSeguirCamara : MonoBehaviour
{
    public float offsetY = -1f;

    void LateUpdate()
    {
        Vector3 cameraTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, Mathf.Abs(Camera.main.transform.position.z)));
        transform.position = new Vector3(transform.position.x, cameraTop.y + offsetY, transform.position.z);
    }
}