using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 制模游戏的数据，主要是切割线的绘制数据，用脚本动态生成线
/// ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "GameData_ZhiMo_SO", menuName = "Data/GameData_ZhiMo")]
public class GameData_ZhiMo : ScriptableObject
{
    [Header("切割线的集合")]
    public List<Connections> lineConnections;
}

[System.Serializable]
public class Connections
{
    public int from;
    public int to;
}
