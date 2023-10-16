using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 刷墨游戏的管理类
/// </summary>
public class GameController : MonoBehaviour
{
    [Header("会播放动画的纸张")]
    public GameObject paper;

    [Header("文本文件")]
    public TextAsset start;
    public GameObject dialogPanel;

    private void Start()
    {
        EventHander.CallDialogEvent(start);
        // dialogPanel.SetActive(true);
        // Invoke("SetFalse", 3f);
    }

    public void SetFalse()
    {
        dialogPanel.SetActive(false);
    }

    //任务完成的按钮方法
    public void MissionComplete()
    {
        StartCoroutine(Paper());
    }

    //协程
    IEnumerator Paper()
    {
        paper.SetActive(true);

        yield return new WaitForSeconds(15f);

        ObjectManager.Instance.isCompleteDict["ShuaMo_Scene"] = true;
        TransitionManager.Instance.StartGameTransition();
    }
}
