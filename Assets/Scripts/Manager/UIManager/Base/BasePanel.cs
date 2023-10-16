using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有UI面包的父类，包括UI面板的状态信息
public class BasePanel
{
    //UI信息
    public UIType UIType{ get; private set;}
    //UI管理工具
    public UITool UITool{ get; private set;}
    //面板管理器
    public PanelManager PanelManager{ get; private set;}
    //UI管理器
    public UIManager UIManager{ get; private set;}

    public BasePanel(UIType uIType)
    {
        UIType = uIType;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize(UITool tool, PanelManager panelManager, UIManager uIManager)
    {
        UITool = tool;
        PanelManager = panelManager;
        UIManager = uIManager;
    }


    //UI进入时执行的操作，只会执行一次
    public virtual void OnEnter(){}

    //UI暂停时执行的操作
    public virtual void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        EventHander.CallGameStateChangeEvent(GameState.Pause);
    }

    //UI继续时执行的操作
    public virtual void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
        EventHander.CallGameStateChangeEvent(GameState.GamePlay);
    }

    //UI退出时执行的操作
    public virtual void OnExit(){}

    /// <summary>
    /// 显示一个面板
    /// </summary>
    /// <param name="panel"></param>
    public void Push(BasePanel panel) => PanelManager?.Push(panel);

    /// <summary>
    /// 关闭一个面板
    /// </summary>
    public void Pop() => PanelManager?.Pop();
}
