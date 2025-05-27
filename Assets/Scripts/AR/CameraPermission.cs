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
            Debug.Log("Solicitando permiso de c�mara...");
            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            Debug.Log("Permiso de c�mara ya concedido.");
        }
#endif
    }
}
