using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    [SerializeField]static readonly string path = "Prefabs/UI/Panel/PausePanel";

    public PausePanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("Continue").onClick.AddListener(() =>
        {
            //点击事件
            Pop();
            
        });

        UITool.GetOrAddComponentInChildren<Button>("Save").onClick.AddListener(() =>
        {
            //点击事件
            EventHander.CallDataSaveEvent();
        });

        UITool.GetOrAddComponentInChildren<Button>("MainMenu").onClick.AddListener(() =>
        {
            //点击事件
            Push(new WarnPanel());
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

    public override void OnExit()
    {
        EventHander.CallGameStateChangeEvent(GameState.GamePlay);
        UIManager.DestroyUI(UIType);
    }
}
