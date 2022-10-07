using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasPlay : UICanvas
{
    public GameObject settingUI;
    public void OnClickSettingButton()
    {
        settingUI.SetActive(true);
    }
}
