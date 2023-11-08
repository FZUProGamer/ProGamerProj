using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildWord : MonoBehaviour
{
    private Word[] wordAry;
    [SerializeField]private List<GameObject> lineList;

    private void Start()
    {
        wordAry = GetComponentsInChildren<Word>();

        int index = 0;

        foreach (var item in wordAry)
        {
            item.Line = lineList[index];
            index++;    
        }
    }
}
