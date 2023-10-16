using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 对话系统，用了DOTween插件和协程来实现打字机的效果，其他代码通过触发DialogEvent事件传入文本文件来显示对话
/// 单例
/// </summary>
public class DialogSystem : Singleton<DialogSystem>
{
    [Header("UI组件")]//显示文本内容的组件
    public GameObject textPanel;
    public Text textLabel;

    [Header("显示文本属性")]
    public int index;
    public float textSpeed;
    private bool isFinished;

    private List<string> textList = new List<string>();//将文本内容切断，按序放入列表中

    //注册和取消事件
    private void OnEnable()
    {
        EventHander.DialogEvent += OnDialogEvent;
    }

    private void OnDisable()
    {
        EventHander.DialogEvent -= OnDialogEvent;
    }

    //外界通过触发事件传入文本文件参数来进入对话
    private void OnDialogEvent(TextAsset textFile)
    {
        //当文本列表为空，是新的对话
        if(textList.Count == 0)
        {
            textPanel.SetActive(true);
            GetFromFile(textFile);
            GetDialogue();
        }
        else
        {
            //不为空代表有对话正在进行
            Debug.Log("有在进行的文本");
        }
    }

    //获取到对话，将文本输入进文本框中
    public void GetDialogue()
    {
        //将游戏状态改为暂停，无法进行其他操作
        EventHander.CallGameStateChangeEvent(GameState.Pause);
        if(textList.Count != 0 && isFinished && index != textList.Count)
        {
            StartCoroutine(SetTextUI());
        }
        else if(textList.Count != 0 && isFinished && index == textList.Count)
        {
            textList.Clear();
            textLabel.text = "";
            textPanel.SetActive(false);
            index = 0;
            //将游戏状态改为开始，可进行其他操作
            EventHander.CallGameStateChangeEvent(GameState.GamePlay);
        }
    }

    //将文本切分进列表中
    private void GetFromFile(TextAsset textFile)
    {
        var lineDate = textFile.text.Split('\n','\r');//按照回车切分文本内容，输出的是数组, var型变量会自动识别类型储存

        foreach(var line in lineDate)
        {
            textList.Add(line);//遍历数组，将数组中的每一个数据存入列表，这样就读取文件了	
        }

        isFinished = true;
    }

    //利用协程和DOTween制作出文本打字机的显示效果
    IEnumerator SetTextUI()
    {
        isFinished = false;
        textLabel.text = "";

        float textDuration = textList[index].Length / textSpeed;
        textLabel.DOText(textList[index], textDuration).SetEase(Ease.Linear);

        yield return new WaitForSeconds(textDuration + 1f);
        isFinished = true;
        index++;
        GetDialogue();
    }
}
