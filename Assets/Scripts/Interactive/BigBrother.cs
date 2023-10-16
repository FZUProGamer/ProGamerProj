using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBrother : Character
{
    WaitForSeconds was = new WaitForSeconds(8f);

    public override void TalkWith()
    {
        am.Play("Talking");

        if(!jianZi_Completed)
        {
            EventHander.CallDialogEvent(jianZi);
            StartCoroutine(Tran("JianZi_Scene", 15f));
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

    IEnumerator Tran(string name, float time)
    {
        yield return new WaitForSeconds(time);
        TransitionManager.Instance.Transition(name);
    }
}
