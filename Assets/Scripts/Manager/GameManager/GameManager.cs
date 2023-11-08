using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 希望在这里实现对整个游戏流程的管理
/// 单例
/// </summary>
public class GameManager : Singleton<GameManager>
{
    //玩家对象的预制体 和 具体生成到场景中的玩家对象
    [SerializeField]private GameObject playerPrefab;
    public GameObject player;
    public GameObject arowPoint;

    [Header("导航信息")]
    public List<TargetTransData_SO> arowPointData;

    //存档系统和数据
    private SaveSystem saveSystem;
    public SaveData saveData;

    public string dataKey = "GameData.sav";

    //注册和取消事件
    private void OnEnable()
    {
        EventHander.DataSaveEvent += OnDataSaveEvent;
    }
    
    private void OnDisable()
    {
        EventHander.DataSaveEvent -= OnDataSaveEvent;
    }

    protected override void Awake()
    {
        base.Awake();
        saveSystem = new SaveSystem();
    }

    //新游戏按钮的具体实现
    public void NewGame()
    {
        ResetData();
        TransitionManager.Instance.StartGameTransition();
    }

    //继续游戏按钮的具体实现
    public void Continue()
    {
        LoadData();
        TransitionManager.Instance.StartGameTransition();
    }

    //退出游戏的具体实现
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 生成并设置玩家的数据方法
    /// </summary>
    public void SetPlayer()
    {
        Quaternion playerRotation = Quaternion.identity;
        Vector3 playerPosition = new Vector3(saveData.playerPosition_x, saveData.playerPosition_y, saveData.playerPosition_z);
        player = Instantiate(playerPrefab, playerPosition, playerRotation);
        arowPoint = player.GetComponent<NewPlayerController>().arowPoint;
        arowPoint.transform.parent = null;
        SetArowPoint();
    }

    public void SetArowPoint()
    {
        TargetTransData_SO data;
        if(!saveData.jianZi_Completed)
        {
            data = arowPointData[0];
            Vector3 point = new Vector3(data.x, data.y, data.z);
            arowPoint.transform.position = point;
        }
        else if(!saveData.chopping_Completed)
        {
            data = arowPointData[1];
            Vector3 point = new Vector3(data.x, data.y, data.z);
            arowPoint.transform.position = point;
        }
        else if(!saveData.zhiMo_Completed)
        {
            data = arowPointData[2];
            Vector3 point = new Vector3(data.x, data.y, data.z);
            arowPoint.transform.position = point;
        }
        else if(!saveData.fanZi_Completed)
        {
            data = arowPointData[3];
            Vector3 point = new Vector3(data.x, data.y, data.z);
            arowPoint.transform.position = point;
        }
        else if(!saveData.keZi_Completed)
        {
            data = arowPointData[4];
            Vector3 point = new Vector3(data.x, data.y, data.z);
            arowPoint.transform.position = point;
        }
    }

    public void SetArowPoint(int index)
    {
        TargetTransData_SO data;
        data = arowPointData[index];
        Vector3 point = new Vector3(data.x, data.y, data.z);
        arowPoint.transform.position = point;
    }

    #region 直接调用的数据保存
    /// <summary>
    /// 保存玩家的数据方法
    /// </summary>
    public void OnDataSaveEvent()
    {
        if(player != null)
        {
            saveData.playerPosition_x = player.transform.position.x;
            saveData.playerPosition_y = player.transform.position.y;
            saveData.playerPosition_z = player.transform.position.z;
        }
    }

    public void SetJianZiData(bool isCompleted)
    {
        saveData.jianZi_Completed = isCompleted;
    }

    public void SetFanZiData(bool isCompleted)
    {
        saveData.fanZi_Completed = isCompleted;
    }

    public void SetZhiMoData(bool isCompleted)
    {
        saveData.zhiMo_Completed = isCompleted;
    }

    public void SetShuaMoData(bool isCompleted)
    {
        saveData.shuaMo_Completed = isCompleted;
    }

    public void SetKeZiData(bool isCompleted)
    {
        saveData.keZi_Completed = isCompleted;
    }

    public void SetChoppingData(bool isCompleted)
    {
        saveData.chopping_Completed = isCompleted;
    }

    public void SetNewGameData(bool isCompleted)
    {
        saveData.newGame = isCompleted;
    }

    /// <summary>
    /// 重置游戏数据
    /// </summary>
    public void ResetData()
    {
        saveData.playerPosition_x = 0f;
        saveData.playerPosition_y = -9.8f;
        saveData.playerPosition_z = -99.33f;
        saveData.jianZi_Completed = false;
        saveData.fanZi_Completed = false;
        saveData.zhiMo_Completed = false;
        saveData.chopping_Completed = false;
        saveData.newGame = true ;
    }
    #endregion


    #region 根本的Json数据保存

    public void SaveData()
    {
        saveSystem.SaveByJson(saveData, dataKey);
    }

    public void LoadData()
    {
        saveSystem.LoadFromJson(saveData, dataKey);
    }
    #endregion
}