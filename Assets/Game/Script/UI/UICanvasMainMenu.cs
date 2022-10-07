using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UICanvasMainMenu : UICanvas
{
    public GameObject playUI;
    public Player playerRef;
    public void OnClickPlayingButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Playing);
        //UIManager.Instance.OpenUI(UIID.Play);
        CloseCanvas();
        playerRef.isPlaying = true;
        playUI.SetActive(true);
    }
}
