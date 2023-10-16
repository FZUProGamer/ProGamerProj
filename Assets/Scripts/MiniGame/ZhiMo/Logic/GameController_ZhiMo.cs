using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 制模游戏的管理类
/// </summary>
public class GameController_ZhiMo : Singleton<GameController_ZhiMo>
{
    [Header("游戏数据")]
    public GameData_ZhiMo gameData;

    [Header("各种对象")]
    public GameObject lineParent;
    public GameObject HmoveLine;
    public GameObject VmoveLine;
    public GameObject line1;
    public GameObject line2;
    public LineRenderer linePrefab;

    [Header("游戏状态")]
    public int targetNum;
    public float moveSpeed;
    public bool toRight;
    public bool toLeft;
    public bool toUp;
    public bool toDown;

    [Header("一些位置信息")]
    public Transform[] pointsTransforms;
    public Transform[] horizontalEdge;
    public Transform[] verticalEdge;

    /// <summary>
    /// 一开始绘制线，向右移动开始
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DrawLines();
        toRight = true;
    }

    /// <summary>
    /// 每帧检查，判断游戏的完成情况和切割线的移动状态
    /// </summary>
    private void Update()
    {
        CheckLine();

        if(targetNum <= 2 && (!toUp && !toDown))
        {
            toRight = false;
            toLeft = false;
            toUp = true;
            HmoveLine.SetActive(false);
            VmoveLine.SetActive(true);
            line1.SetActive(true);
            line2.SetActive(true);
        }
        else if(targetNum == 0)
        {
            ObjectManager.Instance.isCompleteDict["ZhiMo_Scene"] = true;
            TransitionManager.Instance.StartGameTransition();
        }

        if(toRight)
            HMoveLine(moveSpeed);
        else if(toLeft)
            HMoveLine(-moveSpeed);
        else if(toUp)
            VMoveSpeed(moveSpeed);
        else if(toDown)
            VMoveSpeed(-moveSpeed);
    }

    //脚本画线
    public void DrawLines()
    {
        foreach (var connections in gameData.lineConnections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, pointsTransforms[connections.from].position);
            line.SetPosition(1, pointsTransforms[connections.to].position);
        }
    }

    //检测更改切割线的状态
    public void CheckLine()
    {
        if(toRight && HmoveLine.transform.position.x > horizontalEdge[1].position.x)
        {
            toRight = false;
            toLeft = true;
        }
        else if(toLeft && HmoveLine.transform.position.x < horizontalEdge[0].position.x)
        {
            toRight = true;
            toLeft = false;
        }
        else if(toUp && VmoveLine.transform.position.y > verticalEdge[0].position.y)
        {
            toUp = false;
            toDown = true;
        }
        else if(toDown && VmoveLine.transform.position.y < verticalEdge[1].position.y)
        {
            toUp = true;
            toDown = false;
        }
    }

    //线水平移动
    public void HMoveLine(float moveSpeed)
    {
        HmoveLine.transform.position = new Vector3(HmoveLine.transform.position.x + moveSpeed * Time.deltaTime, HmoveLine.transform.position.y, HmoveLine.transform.position.z);
    }

    //线垂直移动
    public void VMoveSpeed(float moveSpeed)
    {
        VmoveLine.transform.position = new Vector3(VmoveLine.transform.position.x, VmoveLine.transform.position.y + moveSpeed * Time.deltaTime, VmoveLine.transform.position.z);
    }
}
