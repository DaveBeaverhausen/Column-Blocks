using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("📦 [SceneDebugger] Escena cargada: " + SceneManager.GetActiveScene().name);

        // Mostrar escenas activas
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var escena = SceneManager.GetSceneAt(i);
            Debug.Log($"🌐 Escena activa [{i}]: {escena.name}");
        }

        // Mostrar objetos marcados como DontDestroyOnLoad
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int countDDOL = 0;
        foreach (var obj in allObjects)
        {
            if (!obj.scene.IsValid() || obj.scene.name == null || obj.scene.name == "")
            {
                Debug.Log($"🔁 Objeto con DontDestroyOnLoad: {obj.name}", obj);
                countDDOL++;
            }
        }

        if (countDDOL == 0)
            Debug.Log("✅ No hay objetos persistentes activos.");
        else
            Debug.Log($"⚠️ Total de objetos con DontDestroyOnLoad: {countDDOL}");
    }
}
