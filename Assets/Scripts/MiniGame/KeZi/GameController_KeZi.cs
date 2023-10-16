using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 刻字游戏的管理类，实现刻字的效果
/// </summary>
public class GameController_KeZi : MonoBehaviour
{
    [Header("刻字效果VFX")]
    public GameObject VFX;

    private Vector3 mouseWorldPos;

    RaycastHit hit;

    float countTime;

    /// <summary>
    /// 每帧检测点击，由射线检测对象
    /// </summary>
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit) && hit.collider.tag== "Split")
        {
            if(Input.GetMouseButton(0))
            {
                mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = -0.05f;
                hit.collider.gameObject.SetActive(false);
                VFX.SetActive(true);
                VFX.transform.position = mouseWorldPos;
            }
        }

        if(countTime >= 1f)
        {
            VFXFalse();
            countTime = 0f;
        }

        countTime += Time.deltaTime;
    }

    private void VFXFalse()
    {
        if(VFX.activeInHierarchy)
        {
            VFX.SetActive(false);
        }
    }

    //完成游戏
    public void Finish()
    {
        ObjectManager.Instance.isCompleteDict["KeZi_Scene"] = true;
        TransitionManager.Instance.Transition("ShuaMo_Scene");
    }
}