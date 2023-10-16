using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏内的面板
/// </summary>
public class GamePanel : BasePanel
{
    [SerializeField]static readonly string path = "Prefabs/UI/Panel/GamePanel";

    public GamePanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("Pause").onClick.AddListener(() =>
        {
            //点击事件
            Push(new PausePanel());
            EventHander.CallGameStateChangeEvent(GameState.Pause);
        });
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
