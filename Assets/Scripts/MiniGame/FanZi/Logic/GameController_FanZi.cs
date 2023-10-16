using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 写反字游戏的管理控制脚本
/// 单例
/// </summary>
public class GameController_FanZi : Singleton<GameController_FanZi>
{
    [Header("游戏数据")]
    public GameData_FanZi gameData;
    public List<SelectAnswer> selectList;

    [Header("题目窗口")]
    public Image Question;
    public List<Image> AnswerList;

    [Header("文本内容")]
    public TextAsset firstStep;
    public TextAsset rightAnswerText;
    public TextAsset wrongAnswerText;
    public float textDuration;

    [Header("题目UI与对话UI")]
    public GameObject gameContent;
    public GameObject dialog;

    [Header("题序")]
    public int nowIndex;
    public int finalIndex;

    public bool canClick;

    protected override void Awake()
    {
        base.Awake();
        finalIndex = gameData.wordsList.Count;
    }

    //开始显示第一题
    void Start()
    {
        StartCoroutine(FirstStep());
    }

    private void OnEnable()
    {
        EventHander.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHander.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canClick = gameState == GameState.GamePlay;
    }

    //显示对话，设置文本
    private void SetText(TextAsset textFile)
    {
        EventHander.CallDialogEvent(textFile);
    }

    //检查，若为正确答案
    public void RightAnswer()
    {
        SetText(rightAnswerText);

        StartCoroutine(Check());
    }

    //检查，若为错误答案
    public void WrongAnswer()
    {
        SetText(wrongAnswerText);
        StartCoroutine(Wait());

        StartCoroutine(SecondStep());
    }

    //协程，设置显示题目
    IEnumerator FirstStep()
    {
        SetText(firstStep);

        Question.sprite = gameData.wordsList[nowIndex].questionImage;

        int num = Random.Range(0,4);
        int index = 0;

        for(int i = 0; i < AnswerList.Count;i++)
        {
            if(i == num)
            {
                AnswerList[num].sprite = gameData.wordsList[nowIndex].correctImage;
                selectList[num].isAnswer = true;
            }
            else
            {
                AnswerList[i].sprite = gameData.wordsList[nowIndex].imageList[index];
                selectList[i].isAnswer = false;
                index++;
            }
        }

        index = 0;
        yield return null;
    }

    IEnumerator SecondStep()
    {
        SetText(firstStep);

        Question.sprite = gameData.wordsList[nowIndex].questionImage;

        int num = Random.Range(0,4);
        int index = 0;

        for(int i = 0; i < AnswerList.Count;i++)
        {
            if(i == num)
            {
                AnswerList[num].sprite = gameData.wordsList[nowIndex].correctImage;
                selectList[num].isAnswer = true;
            }
            else
            {
                AnswerList[i].sprite = gameData.wordsList[nowIndex].imageList[index];
                selectList[i].isAnswer = false;
                index++;
            }
        }

        index = 0;

        yield return null;
    }

    //协程，检查所有题目是否都做完了
    IEnumerator Check()
    {
        yield return new WaitForSeconds(4.5f);

        nowIndex++;

        if(nowIndex >= finalIndex)
        {
            ObjectManager.Instance.isCompleteDict["FanZi_Scene"] = true;
            TransitionManager.Instance.StartGameTransition();
        }
        else
            yield return SecondStep();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(textDuration);
    }
}
