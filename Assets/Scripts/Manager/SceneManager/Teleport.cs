using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载在用于互动传送场景的物体上，需要把该物体的 TAG 设置为Teleport
/// 同时，为了能够显示互动按钮，需要给它一个按钮的Prefab和需要显示的位置
/// </summary>
public class Teleport : MonoBehaviour
{
    [Header("文本文件")]
    public TextAsset nonGameTextFile;
    public TextAsset cantTextFile;

    [Header("场景")]
    public string sceneTo;
    public bool isComplete;
    public bool canPlay;

    [Header("上级任务")]
    public Teleport lastLevel;
    public string lastLevel_key;

    private void Update()
    {
        if(lastLevel is not null)
            canPlay = lastLevel.isComplete;
        else
        {
            canPlay = ObjectManager.Instance.isCompleteDict[lastLevel_key];
        }
    }

    //玩家通过这个方法来进入到新的场景中
    public void TeleportToScene()
    {        
        if(!isComplete && canPlay)
            TransitionManager.Instance.Transition(sceneTo);
        else if(!canPlay)
        {
            EventHander.CallDialogEvent(cantTextFile);
        }
        else
        {
            EventHander.CallDialogEvent(nonGameTextFile);
        }
    }
}
