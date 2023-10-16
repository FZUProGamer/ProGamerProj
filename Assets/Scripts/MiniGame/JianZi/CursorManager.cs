using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主要是检测鼠标点击的坐标来获取点击的对象
/// </summary>
public class CursorManager : MonoBehaviour
{
    //鼠标的世界坐标
    private Vector3 mouseWorldPos;

    //鼠标有碰到物体才能点击
    private bool canClick;

    private Word word1;

    //每帧都更新鼠标的坐标和点击状态
    private void Update() 
    {
        mouseWorldPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        canClick = ObjectAtMousePosition();

        if(canClick && ObjectAtMousePosition().gameObject.tag is "Word")
        {
            var word2 = ObjectAtMousePosition().gameObject.GetComponent<Word>();
            word1?.DisableLine();
            word2?.ShowLine();
            word1 = word2;
        }

        if(canClick && Input.GetMouseButtonDown(0))
        {
            //检测鼠标互动情况
            ClickAction(ObjectAtMousePosition().gameObject);
            EventHander.CallUIAudioEvent();
        }
    }

    /// <summary>
    /// 通过Tag检索鼠标点击不同物品时触发的事件
    /// </summary>
    /// <param name="clickObject">点击的物体</param>
    private void ClickAction(GameObject clickObject)
    {
        switch(clickObject.tag)
        {
            case "Word":
                var word = clickObject.GetComponent<Word>();
                word?.SetCelected();
                break;
        }
    }

    /// <summary>
    /// 检测鼠标点击范围的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
