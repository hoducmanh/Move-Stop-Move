using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isFirstLoad = true;
    public static event Action<GameState> OnGameStateChange;
    public GameState CurrentGameState { get; private set; } = GameState.InitState;
    public GameState PrevGameState { get; private set; }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                OnGameStateMainMenu();
                break;
            case GameState.Playing:
                OnGameStatePlaying();
                break;
            case GameState.Pause:
                OnGameStatePause();
                break;
            case GameState.Result:
                OnGameStateResultPhase();
                break;
            case GameState.WeaponShop:
                OnGameStateWeaponShop();
                break;
            case GameState.SkinShop:
                OnGameStateSkinShop();
                break;
            case GameState.Revive:
                OnGameStateReviveOption();
                break;
            default:
                break;
        }
        AutoSetTimeScale(state);
        PrevGameState = CurrentGameState;
        CurrentGameState = state;

        OnGameStateChange?.Invoke(state);
    }
    private void OnGameStateMainMenu()
    {
        if (isFirstLoad)
        {
            UIManager.Instance.OpenUI(UIID.Menu);
            isFirstLoad = false;
        }
    }
    private void OnGameStatePlaying()
    {
    }
    private void OnGameStatePause()
    {
    }
    private void OnGameStateResultPhase()
    {

    }
    private void OnGameStateWeaponShop()
    {

    }
    private void OnGameStateSkinShop()
    {

    }
    private void OnGameStateReviveOption()
    {

    }
    private void OnGameStateEndGame()
    {
    }
    private void AutoSetTimeScale(GameState state)
    {
        if (state == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
}
public enum GameState
{
    InitState,
    LoadLevel,
    MainMenu,
    Pause,
    StartGame,
    Playing,
    Result,
    EndGame,
    Revive,
    WeaponShop,
    SkinShop
}
