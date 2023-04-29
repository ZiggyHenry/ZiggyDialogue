using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;
    public AudioClip dialogueStartSfx;

    private AudioClip currentDialogueAudio;
    private bool sfxAudioPlaying = false;

    void Start()
    {
        EvtSystem.EventDispatcher.AddListener<PlayAudio>(PlayAudioClip);
    }

    private void PlayAudioClip(PlayAudio data)
    {
        audioSource.Stop();
        if ( audioSource != null )
        {
            audioSource.PlayOneShot(dialogueStartSfx);
            currentDialogueAudio = data.clipToPlay;

            sfxAudioPlaying = true;
        }
    }

    public void Update()
    {
        if (!audioSource.isPlaying && audioSource.clip == dialogueStartSfx && 
            sfxAudioPlaying)
        {
            audioSource.PlayOneShot(currentDialogueAudio);
        }
    }
}
