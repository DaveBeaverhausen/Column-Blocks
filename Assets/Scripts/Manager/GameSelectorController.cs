using UnityEngine;

public class GameSelectorController : MonoBehaviour
{
    public void IrAEspartano() => SceneLoader.Instance.LoadScene("PreObjects");
    public void IrAColumnas() => SceneLoader.Instance.LoadScene("PreColumns");
    public void IrAVR() => SceneLoader.Instance.LoadScene("ErrorVR");
    public void IrAAR() => SceneLoader.Instance.LoadScene("PreAR");
}