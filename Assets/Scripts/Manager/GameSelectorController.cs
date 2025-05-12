using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectorController : MonoBehaviour
{
    public void IrAEspartano() => SceneLoader.Instance.LoadScene("SpartanGame");
    public void IrAColumnas() => SceneLoader.Instance.LoadScene("ColumnsGame");
    public void IrAVR() => SceneLoader.Instance.LoadScene("VRGame");
    public void IrAAR() => SceneLoader.Instance.LoadScene("ARGame");
}