using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载到每个字上，设置这个字是否为正确答案和是否被选择
/// </summary>
public class Word : MonoBehaviour
{
    public bool incorrect;
    public bool isCelected;

    [Header("选中的图标")]
    public GameObject X;
    public GameObject Line;

    private void Awake()
    {
        X = gameObject.transform.GetChild(0).gameObject;
    }

    public void SetCelected()
    {
        isCelected = !isCelected;

        X.SetActive(isCelected);
    }

    public void ShowLine()
    {
        Line.SetActive(true);
    }

    public void DisableLine()
    {
        if(Line.activeInHierarchy)
        {
            Line.SetActive(false);
        }
    }
}