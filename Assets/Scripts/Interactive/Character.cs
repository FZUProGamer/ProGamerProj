using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC角色的脚本，主要是通过这个来让玩家触发激活不同的任务对话
/// </summary>
public class Character : MonoBehaviour
{
    //动画器
    public Animator am;

    //几种任务的完成情况
    protected bool jianZi_Completed => GameManager.Instance.saveData.jianZi_Completed;
    protected bool fanZi_Completed => GameManager.Instance.saveData.fanZi_Completed;
    protected bool chopping_Completed => GameManager.Instance.saveData.chopping_Completed;
    protected bool zhiMo_Completed => GameManager.Instance.saveData.zhiMo_Completed;
    protected bool keZhi_Completed => GameManager.Instance.saveData.keZi_Completed;
    protected bool shuaMo_Completed => GameManager.Instance.saveData.shuaMo_Completed;

    //对话文本文件
    [SerializeField]protected TextAsset jianZi;
    [SerializeField]protected  TextAsset chop;
    [SerializeField]protected  TextAsset zhiMo;
    [SerializeField]protected  TextAsset fanZi;
    [SerializeField]protected  TextAsset keZi;
    [SerializeField]protected  TextAsset final;

    //一开始从数据中获取任务完成情况
    private void Awake()
    {
        am = GetComponent<Animator>();
    }

    public virtual void TalkWith(){}
}
