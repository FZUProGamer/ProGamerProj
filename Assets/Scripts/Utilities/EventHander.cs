using System;
using UnityEngine;

/// <summary>
/// 事件中心
/// 观察者模式
/// 静态
/// </summary>
public static class EventHander
{
    #region 游戏状态事件
    public static event Action<GameState> GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action GamePlayEvent;
    public static void CallGamePlayEvent()
    {
        GamePlayEvent?.Invoke();
    }

    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }
    #endregion
    
    #region 对话系统事件
    public static event Action<TextAsset> DialogEvent;
    public static void CallDialogEvent(TextAsset textFile)
    {
        DialogEvent?.Invoke(textFile);
    }
    #endregion

    #region 音频系统事件
    public static event Action<string> PlayerAudioEvent;
    public static void CallPlayerAudioEvent(string type)
    {
        PlayerAudioEvent?.Invoke(type);
    }

    public static event Action MusicAudioEvent;
    public static void CallMusicAudioEvent()
    {
        MusicAudioEvent?.Invoke();
    }

    public static event Action AmbientAudioEvent;
    public static void CallAmbientAudioEvent()
    {
        AmbientAudioEvent?.Invoke();
    }

    public static event Action UIAudioEvent;
    public static void CallUIAudioEvent()
    {
        UIAudioEvent?.Invoke();
    }

    public static event Action<string> StopAudioPlayEvent;
    public static void CallStopAudioPlayEvent(string audioSource)
    {
        StopAudioPlayEvent?.Invoke(audioSource);
    }
    #endregion

    #region 游戏存档和读档事件
    public static event Action DataLoadEvent;
    public static void CallDataLoadEvent()
    {
        DataLoadEvent?.Invoke();
    }

    public static event Action DataSaveEvent;
    public static void CallDataSaveEvent()
    {
        DataSaveEvent?.Invoke();
    }
    #endregion

}