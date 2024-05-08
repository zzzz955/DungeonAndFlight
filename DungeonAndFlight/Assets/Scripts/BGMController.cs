using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioClip[] bgmClips;
    private int currentBGMIndex = 0;

    void Start()
    {
        PlayBGM(currentBGMIndex);
    }

    public void PlayBGM(int index)
    {
        if (bgmAudioSource != null && index >= 0 && index < bgmClips.Length)
        {
            bgmAudioSource.clip = bgmClips[index];
            bgmAudioSource.Play();
        }
    }

    public void NextLevel()
    {
        bgmAudioSource.Stop();
        currentBGMIndex += 1;
        PlayBGM(currentBGMIndex);
    }

    private void Update()
    {
        if (!bgmAudioSource.isPlaying)
        {
            // 노래가 끝나면 처음부터 재생
            PlayBGM(0);
        }
    }
}
