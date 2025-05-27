using UnityEngine;
using UnityEngine.XR.ARFoundation;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class ARController : MonoBehaviour
{
    public GameObject arSession;
    public GameObject arSessionOrigin;

    void OnEnable()
    {
        Debug.Log("ARController activado.");

        // Activar AR
        arSession.SetActive(true);
        arSessionOrigin.SetActive(true);

        // Pedir permiso de cámara si es Android
#if UNITY_ANDROID
        StartCoroutine(PedirPermisoCamara());
#endif
    }

    void OnDisable()
    {
        arSession.SetActive(false);
        arSessionOrigin.SetActive(false);
    }

#if UNITY_ANDROID
    System.Collections.IEnumerator PedirPermisoCamara()
    {
        yield return new WaitForSeconds(1f); // espera 1 segundo

        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log("Solicitando permiso de cámara...");
            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            Debug.Log("Permiso de cámara ya concedido.");
        }
    }
#endif
}
