using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理那几个Timeline的脚本，也是土办法
/// 每次切换回主游戏场景，都会来判断当前任务
/// </summary>
public class TimeLine : MonoBehaviour
{
    public bool newGame;
    public bool finishGame;

    public TextAsset teachText;

    public GameObject teachPanel;
    public Teacher teacher;
    public GameObject finalMission;

    private void Awake()
    {
        newGame = GameManager.Instance.saveData.newGame;

        if(!GameManager.Instance.saveData.newGame && GameManager.Instance.saveData.jianZi_Completed && GameManager.Instance.saveData.zhiMo_Completed && GameManager.Instance.saveData.fanZi_Completed && GameManager.Instance.saveData.keZi_Completed && GameManager.Instance.saveData.shuaMo_Completed)
            finishGame = true;
    }

    private void Start()
    {
        if(newGame)
        {
            teachPanel.SetActive(true);
            EventHander.CallDialogEvent(teachText);
            Invoke("FirstMission",10.5f);
            ObjectManager.Instance.isCompleteDict["NewGame"] = false;
        }
        else if(finishGame)
            finalMission.SetActive(true);
    }

    private void FirstMission()
    {
        teachPanel.SetActive(false);
        teacher.NewGame();
    }
}
