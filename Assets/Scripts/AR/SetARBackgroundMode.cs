using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class SetARBackgroundMode : MonoBehaviour
{
    void Start()
    {
        var camManager = GetComponent<ARCameraManager>();
        if (camManager != null)
            camManager.requestedBackgroundRenderingMode = CameraBackgroundRenderingMode.AfterOpaques;
    }
}