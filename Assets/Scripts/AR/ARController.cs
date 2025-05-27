using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{
    public GameObject arSession;
    public GameObject arSessionOrigin;

    void OnEnable()
    {
        arSession.SetActive(true);
        arSessionOrigin.SetActive(true);
    }

    void OnDisable()
    {
        arSession.SetActive(false);
        arSessionOrigin.SetActive(false);
    }
}
