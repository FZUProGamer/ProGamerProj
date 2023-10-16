using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基本的保存系统脚本
/// Json
/// 哈希列表
/// </summary>
public class SaveSystem
{
    //将数据转成Json格式，保存到本地
    public void SaveByJson(Object data, string key)
    {
        var json = JsonUtility.ToJson(data, true);
        
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    //从本地读取数据，将Json转换回来
    public void LoadFromJson(Object data, string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
}
