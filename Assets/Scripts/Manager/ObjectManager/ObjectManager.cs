using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 专门用来记录各物体的状态的管理类
/// 字典
/// 单例
/// </summary>
public class ObjectManager : Singleton<ObjectManager>
{
    //用来记录数据的字典，key是名字，要存储的是布尔值，保存互动信息
    public Dictionary<string, bool> isCompleteDict = new Dictionary<string, bool>();

    //在游戏一开始把所有的物体先默认注册进字典中
    protected override void Awake()
    {
        base.Awake();
        isCompleteDict.Add("NewGame", true);
        isCompleteDict.Add("FanZi_Scene", false);
        isCompleteDict.Add("JianZi_Scene", false);
        isCompleteDict.Add("ZhiMo_Scene", false);
        isCompleteDict.Add("Chopping", false);
        isCompleteDict.Add("KeZi_Scene", false);
        isCompleteDict.Add("ShuaMo_Scene", false);
    }

    //注册和取消事件
    private void OnEnable()
    {
        EventHander.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHander.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHander.DataLoadEvent += OnDataLoadEvent;
        EventHander.DataSaveEvent += OnDataSaveEvent;
    }

    private void OnDisable()
    {
        EventHander.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHander.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHander.DataLoadEvent -= OnDataLoadEvent;
        EventHander.DataSaveEvent -= OnDataSaveEvent;
    }

    //在旧场景卸载之前保存完当前场景的物体信息进字典中
    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Teleport>())
        {
            if(!isCompleteDict.ContainsKey(item.sceneTo))
                isCompleteDict.Add(item.sceneTo, false);
            else
                isCompleteDict[item.sceneTo] = item.isComplete;
        }
    }

    //在新场景加载完成之后讲字典中的物体信息设置到对应物体中
    private void OnAfterSceneLoadedEvent()
    {
        foreach (var item in FindObjectsOfType<Teleport>())
        {
            if(!isCompleteDict.ContainsKey(item.sceneTo))
                isCompleteDict.Add(item.sceneTo, false);
            else
                item.isComplete = isCompleteDict[item.sceneTo];
        }
    }

    //数据加载事件，将本地保存的数据覆盖到字典中
    private void OnDataLoadEvent()
    {
        if(!isCompleteDict.ContainsKey("NewGame"))
            isCompleteDict.Add("NewGame", GameManager.Instance.saveData.newGame);
        else
            isCompleteDict["NewGame"] = GameManager.Instance.saveData.newGame;

        if(!isCompleteDict.ContainsKey("FanZi_Scene"))
            isCompleteDict.Add("FanZi_Scene", GameManager.Instance.saveData.fanZi_Completed);
        else
            isCompleteDict["FanZi_Scene"] = GameManager.Instance.saveData.fanZi_Completed;

        if(!isCompleteDict.ContainsKey("JianZi_Scene"))
            isCompleteDict.Add("JianZi_Scene", GameManager.Instance.saveData.jianZi_Completed);
        else
            isCompleteDict["JianZi_Scene"] = GameManager.Instance.saveData.jianZi_Completed;

        if(!isCompleteDict.ContainsKey("ZhiMo_Scene"))
            isCompleteDict.Add("ZhiMo_Scene", GameManager.Instance.saveData.zhiMo_Completed);
        else
            isCompleteDict["ZhiMo_Scene"] = GameManager.Instance.saveData.zhiMo_Completed;

        if(!isCompleteDict.ContainsKey("Chopping"))
            isCompleteDict.Add("Chopping", GameManager.Instance.saveData.chopping_Completed);
        else
            isCompleteDict["Chopping"] = GameManager.Instance.saveData.chopping_Completed;

        if(!isCompleteDict.ContainsKey("KeZi_Scene"))
            isCompleteDict.Add("KeZi_Scene", GameManager.Instance.saveData.keZi_Completed);
        else
            isCompleteDict["KeZi_Scene"] = GameManager.Instance.saveData.keZi_Completed;


        if(!isCompleteDict.ContainsKey("ShuaMo_Scene"))
            isCompleteDict.Add("ShuaMo_Scene", GameManager.Instance.saveData.shuaMo_Completed);
        else
            isCompleteDict["ShuaMo_Scene"] = GameManager.Instance.saveData.shuaMo_Completed;
    }

    //数据保存事件，将字典中的数据覆盖保存到本地
    private void OnDataSaveEvent()
    {
        GameManager.Instance.SetFanZiData(isCompleteDict["FanZi_Scene"]);
        GameManager.Instance.SetJianZiData(isCompleteDict["JianZi_Scene"]);
        GameManager.Instance.SetZhiMoData(isCompleteDict["ZhiMo_Scene"]);
        GameManager.Instance.SetChoppingData(isCompleteDict["Chopping"]);
        GameManager.Instance.SetNewGameData(isCompleteDict["NewGame"]);
        GameManager.Instance.SetShuaMoData(isCompleteDict["ShuaMo_Scene"]);
        GameManager.Instance.SetKeZiData(isCompleteDict["KeZi_Scene"]);
    }
}
