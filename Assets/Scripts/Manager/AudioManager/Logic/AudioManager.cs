using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource playerSource;//玩家音源
    AudioSource NPCSource;//NPC音源
    AudioSource musicSource;//音乐音源
    AudioSource ambientSource;//环境音效音源
    AudioSource uiSource;//UI音效音源

    [Header("音效文件_玩家")]
    public AudioClip chopping;
    public AudioClip walking;
    [Header("音效文件_环境")]
    public AudioClip ambient;
    [Header("音效文件_BGM")]
    public AudioClip music;
    [Header("音效文件_UI")]
    public AudioClip btn;
    [Header("音效文件_打墨")]
    public AudioClip daMo;
    [Header("音效文件_制模")]
    public AudioClip zhiMo;

    #region 添加处理
    private void Awake()
    {
        playerSource = gameObject.AddComponent<AudioSource>();
        NPCSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        ambientSource = gameObject.AddComponent<AudioSource>();
        uiSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventHander.PlayerAudioEvent += OnPlayerAudioEvent;
        EventHander.MusicAudioEvent += OnMusicAudioEvent;
        EventHander.AmbientAudioEvent += OnAmbientAudioEvent;
        EventHander.UIAudioEvent += OnUIAudioEvent;

        EventHander.StopAudioPlayEvent += OnStopAudioPlayEvent;
    }

    private void OnDisable()
    {
        EventHander.PlayerAudioEvent -= OnPlayerAudioEvent;
        EventHander.MusicAudioEvent -= OnMusicAudioEvent;
        EventHander.AmbientAudioEvent -= OnAmbientAudioEvent;
        EventHander.UIAudioEvent -= OnUIAudioEvent;

        EventHander.StopAudioPlayEvent -= OnStopAudioPlayEvent;
    }
    #endregion

    #region 播放不同音效的方法
    //播放玩家音效
    private void OnPlayerAudioEvent(string action)
    {
        switch(action)
        {
            case "walking" :
                AudioPlay(playerSource, walking, false);
                break;
            case "chopping" :
                AudioPlay(playerSource, chopping, true);
                break;
            case "zhiMo" :
                AudioPlayOneShot(playerSource, zhiMo);
                break;
            case "daMo" :
                AudioPlay(playerSource, daMo, false);
                break;
        }
    }

    //播放背景音乐
    private void OnMusicAudioEvent()
    {
        AudioPlay(musicSource, music, true);
    }

    //播放背景音效
    private void OnAmbientAudioEvent()
    {
        AudioPlay(ambientSource, ambient, true);
    }

    //播放ui按钮音效
    private void OnUIAudioEvent()
    {
        AudioPlay(uiSource, btn, false);
    }

    //设置音源并播放
    private void AudioPlay(AudioSource audioSource, AudioClip audioClip, bool isLoop)
    {
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.Play();
    }

    //只播放一次
    private void AudioPlayOneShot(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.PlayOneShot(audioClip);
    }

    #endregion

    private void OnStopAudioPlayEvent(string audioSource)
    {
        switch(audioSource)
        {
            case "playerSource" :
                playerSource.Stop();
                break;
            case "NPCSource" :
                NPCSource.Stop();
                break;
            case "musicSource" :
                musicSource.Stop();
                break;
            case "ambientSource" :
                ambientSource.Stop();
                break;
            case "uiSource" :
                uiSource.Stop();
                break;
        }
    }


    public void PlayUIAudio()
    {
        OnUIAudioEvent();
    }
}
