using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CameraPermissionRequester : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log("Solicitando permiso de cámara...");
            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            Debug.Log("Permiso de cámara ya concedido.");
        }
#endif
    }
}
