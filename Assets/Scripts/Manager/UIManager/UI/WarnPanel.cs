using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarnPanel : BasePanel
{
    [SerializeField]static readonly string path = "Prefabs/UI/Panel/WarnPanel";

    public WarnPanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("True").onClick.AddListener(() =>
        {
            //点击事件
            TransitionManager.Instance.Transition("MainMenu");

        });

        UITool.GetOrAddComponentInChildren<Button>("False").onClick.AddListener(() =>
        {
            //点击事件
            Pop();
        });
    }

    
    public override void OnExit()
    {
        UIManager.DestroyUI(UIType);
    }
}
