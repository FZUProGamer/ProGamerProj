using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [Header("文本文件")]
    public TextAsset finishGame;

    private void OnEnable()
    {
        EventHander.CallDialogEvent(finishGame);
        StartCoroutine(gameFinish());
    }

    IEnumerator gameFinish()
    {
        yield return new WaitForSeconds(10f);

        GameManager.Instance.ResetData();
        TransitionManager.Instance.Transition("MainMenu");
    }
}
