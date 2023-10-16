using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触发虚线的脚本
/// </summary>
public class Line : MonoBehaviour
{
    private bool isTouch;

    [Header("重要的对象")]
    public GameObject destroyObject;
    private GameObject destroyLine;
    public GameObject lineParent;

    public int index;

    //直接获取线
    private void Start()
    {
        destroyLine = lineParent.transform.GetChild(index).gameObject;
    }

    /// <summary>
    /// 每帧检测切割线是否进入可切割的范围
    /// </summary>
    private void Update()
    {
        if(isTouch && Input.GetKeyDown(KeyCode.E))
        {
            GameController_ZhiMo.Instance.targetNum--;
            EventHander.CallPlayerAudioEvent("zhiMo");
            Destroy(destroyObject);
            Destroy(destroyLine);
            Destroy(this);
        }
    }

    //有触发器进入，就是切割线进入了
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Line"))
        {
            Debug.Log("触碰到线了");
            isTouch = true;
        }
    }

    //切割线离开了
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Line"))
        {
            Debug.Log("线离开了");
            isTouch = false;
        }
    }
}
