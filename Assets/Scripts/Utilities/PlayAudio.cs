using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private void Start()
    {
        EventHander.CallAmbientAudioEvent();
    }

    public void PlayUIAudio()
    {
        EventHander.CallUIAudioEvent();
    }
}
