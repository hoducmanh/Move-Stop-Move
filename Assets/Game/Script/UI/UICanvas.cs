using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIID
{
    Menu,
    Play,
    Setting,
    SkinStore,
    WeaponStore,
    End
}
public class UICanvas : MonoBehaviour
{
    public GameObject currCanvas;
    public void OpenCanvas()
    {
        currCanvas.SetActive(true);
    }
    public void CloseCanvas()
    {
        currCanvas.SetActive(false);
    }
}
