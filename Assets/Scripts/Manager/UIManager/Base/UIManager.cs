using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储所有UI的信息，并可以创建或者销毁
/// </summary>
public class UIManager
{
    //存储所有UI的字典，每个UI信息都会对应一个GameObject
    private Dictionary<UIType, GameObject> dicUI;

    public UIManager()
    {
        dicUI = new Dictionary<UIType, GameObject>();
    }

    /// <summary>
    /// 获取一个UI对象
    /// </summary>
    /// <param name="type">UI信息</param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("MainCanvas");

        if(!parent)
        {
            Debug.LogError("Canvas不存在,请仔细查找有无这个对象");
            return null;
        }

        if(dicUI.ContainsKey(type))
            return dicUI[type];
        
        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        ui.name = type.Name;
        dicUI.Add(type, ui);

        return ui;
    }

    public void DestroyUI(UIType type)
    {
        if(dicUI.ContainsKey(type))
        {
            GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }
}