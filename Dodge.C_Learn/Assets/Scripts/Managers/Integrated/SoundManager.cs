using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxSource;       //sfx전용 오디오 변수
    public AudioSource bgmSource;       //bgm전용 오디오 변수

    private Dictionary<SfxType, AudioClip> sfxClipDic = new Dictionary<SfxType, AudioClip>();       //sfx클립 저장해놓는 Dic
    private Dictionary<BgmType, AudioClip> bgmClipDic = new Dictionary<BgmType, AudioClip>();       //bgm클립 저장해놓는 Dic

    /// <summary>
    /// SFX 재생 함수
    /// </summary>
    public void PlaySFX(SfxType type)
    {
        sfxSource.PlayOneShot(sfxClipDic[type]);
    }

    /// <summary>
    /// BGM 재생 함수
    /// </summary>
    public void PlayBGM(BgmType type)
    {
        if (bgmSource.clip == bgmClipDic[type])
            return;

        bgmSource.clip = bgmClipDic[type];
        bgmSource.Play();
    }

    public void OnLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Title":
                if (bgmSource.clip == null || bgmSource.clip != bgmClipDic[BgmType.Title])
                {
                    bgmSource.clip = bgmClipDic[BgmType.Title];
                    bgmSource.Play();
                }
                break;
            case "InGame":
                if (bgmSource.clip == null || bgmSource.clip != bgmClipDic[BgmType.InGame])
                {
                    bgmSource.clip = bgmClipDic[BgmType.InGame];
                    bgmSource.Play();
                }
                break;
        }
    }

    /// <summary>
    /// SoundManager 생성 함수
    /// </summary>
    public void Init()
    {
        GameObject sfxGo = new GameObject("SFX");
        GameObject bgmGo = new GameObject("BGM");

        sfxGo.transform.SetParent(transform);
        bgmGo.transform.SetParent(transform);

        sfxSource = sfxGo.AddComponent<AudioSource>();
        bgmSource = bgmGo.AddComponent<AudioSource>();

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        sfxSource.playOnAwake = false;

        var sfxClipArr = Resources.LoadAll<AudioClip>("Sounds/SFX");
        ClipLoader(ref sfxClipDic, sfxClipArr);

        var bgmClipArr = Resources.LoadAll<AudioClip>("Sounds/BGM");
        ClipLoader(ref bgmClipDic, bgmClipArr);

        Managers.Scene.OnLoadCompleted(OnLoadCompleted);
    }

    /// <summary>
    /// 클립 읽어주는 함수
    /// </summary>
    public void ClipLoader<T>(ref Dictionary<T, AudioClip> dic, AudioClip[] arr) where T : Enum
    {
        for (int i = 0; i < arr.Length; i++)
        {
            try
            {
                T type = (T)Enum.Parse(typeof(T), arr[i].name);
                dic.Add(type, arr[i]);
            }
            catch
            {
                Debug.LogWarning("Need Enum : " + arr[i].name);
            }
        }
    }
}
