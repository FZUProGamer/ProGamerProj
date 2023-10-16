using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : Character
{
    WaitForSeconds was = new WaitForSeconds(7f);

    public override void TalkWith()
    {
        am.Play("Talking");

        if(!jianZi_Completed)
        {
            EventHander.CallDialogEvent(jianZi);
        }
        else if(jianZi_Completed && !chopping_Completed)
        {
            EventHander.CallDialogEvent(chop);
        }
        else if(chopping_Completed && !zhiMo_Completed)
        {
            EventHander.CallDialogEvent(zhiMo);
        }
        else if(zhiMo_Completed && !keZhi_Completed)
        {
            EventHander.CallDialogEvent(keZi);            
        }

        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        yield return was;
        am.Play("Idle");
    }

    public void NewGame()
    {
        am.Play("Talking");
        EventHander.CallDialogEvent(jianZi);
        StartCoroutine(Idle());
    }
}