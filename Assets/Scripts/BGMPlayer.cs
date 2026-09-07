using UnityEngine;
using System.Collections;
using System;

public class BGMPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip BGM_DogBark;
    public AudioClip BGM_Intro;
    public AudioClip BGM_Loop;

    public AudioSource audioSource;
    public float BGMVolume = 0.8f;

    void Start()
    {
        audioSource.volume = BGMVolume;
        StartCoroutine(BGMManager());
    }

    IEnumerator BGMManager()
    {
        //audioSource.clip = BGM_DogBark;
        //audioSource.Play();
        //yield return new WaitForSeconds(BGM_DogBark.length);

        audioSource.clip = BGM_Intro;
        audioSource.Play();

        yield return new WaitForSeconds(BGM_Intro.length);

        audioSource.clip = BGM_Loop;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
