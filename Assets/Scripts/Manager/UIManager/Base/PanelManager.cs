using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理器，用栈来存储UI
/// </summary>
public class PanelManager
{
    //用栈来存储UI面板
    private Stack<BasePanel> stackPanel;

    //UI管理器
    private UIManager uIManager;
    private BasePanel panel;

    public PanelManager()
    {
        stackPanel = new Stack<BasePanel>();
        uIManager = new UIManager();
    }

    /// <summary>
    /// UI的入栈操作，此操作会显示一个面板
    /// </summary>
    /// <param name="nextPanel">要显示的面板</param>
    public void Push(BasePanel nextPanel)
    {
        if(stackPanel.Count > 0)
        {
            panel = stackPanel.Peek();
            panel.OnPause();
        }
        stackPanel.Push(nextPanel);
        GameObject panelGo = uIManager.GetSingleUI(nextPanel.UIType);
        nextPanel.Initialize(new UITool(panelGo), this, uIManager);
        nextPanel.OnEnter();
    }

    public void Pop()
    {
        if(stackPanel.Count > 0)
            stackPanel.Pop().OnExit();
        if(stackPanel.Count > 0)
            stackPanel.Peek().OnResume();
    }

    /// <summary>
    /// 执行所有面板的OnExit
    /// </summary>
    public void PopAll()
    {
        while(stackPanel.Count > 0)
            stackPanel.Pop().OnExit();
    }
}