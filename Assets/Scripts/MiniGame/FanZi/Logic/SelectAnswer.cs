using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这个脚本挂载在显示题目的按钮上，点击按钮会判断是不是正确答案，执行不同的方法
/// </summary>
public class SelectAnswer : MonoBehaviour
{
    public bool isAnswer;

    public void checkAnswer()
    {
        if(GameController_FanZi.Instance.canClick)
        {
            EventHander.CallUIAudioEvent();
            if(isAnswer)
                GameController_FanZi.Instance.RightAnswer();
            else
                GameController_FanZi.Instance.WrongAnswer();
        }
    }
}
