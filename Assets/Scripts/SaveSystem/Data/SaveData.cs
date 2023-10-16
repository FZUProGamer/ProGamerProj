using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 具体的数据内容
/// ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "GameData_Main", menuName = "Data/GameData_Main")]
public class SaveData : ScriptableObject
{
    public float playerPosition_x;
    public float playerPosition_y;
    public float playerPosition_z;

    public bool newGame;
    public bool jianZi_Completed;
    public bool chopping_Completed;
    public bool zhiMo_Completed;
    public bool fanZi_Completed;
    public bool keZi_Completed;
    public bool shuaMo_Completed;
}