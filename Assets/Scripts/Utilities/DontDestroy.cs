using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在场景中不被移除
/// </summary>
public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
