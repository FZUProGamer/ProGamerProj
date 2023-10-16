using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检字游戏的管理类
/// </summary>
public class JianZiManager : MonoBehaviour
{
    //正确答案的字列表
    public List<Word> wordList;

    //判断游戏完成
    private bool isComplete = false;

    [Header("对话文本文件")]
    public TextAsset gameStartFile;
    public TextAsset checkWrongFile;
    public TextAsset checkRightFile;

    private void Start()
    {
        EventHander.CallDialogEvent(gameStartFile);
    }

    /// <summary>
    /// 检查任务是否完成了
    /// </summary>
    public void checkWords()
    {
        int index = 0;
        foreach (var item in wordList)
        {
            if(item.isCelected)
                index++;
        }

        if(index == wordList.Count)
            isComplete = true;

        if(isComplete)
        {
            Debug.Log("完成了任务！");

            StartCoroutine(Right());
        }
        else
        {
            EventHander.CallDialogEvent(checkWrongFile);
        }
        index = 0;
    }

    /// <summary>
    /// 协程，完成游戏后显示对话，对话结束后回到主场景
    /// </summary>
    /// <returns></returns>
    IEnumerator Right()
    {
        EventHander.CallDialogEvent(checkRightFile);

        yield return new WaitForSeconds(5f);

        ObjectManager.Instance.isCompleteDict["JianZi_Scene"] = true;
        TransitionManager.Instance.StartGameTransition();
    }
}
