using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 写反字游戏的数据脚本，题库
/// ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "GameData_FanZi_SO", menuName = "Data/GameData_FanZi")]
public class GameData_FanZi : ScriptableObject
{
    [Header("选字题库")]
    public List<Words> wordsList;
}

[System.Serializable]
public class Words
{
    public Sprite questionImage;
    public Sprite correctImage;
    public List<Sprite> imageList;
}