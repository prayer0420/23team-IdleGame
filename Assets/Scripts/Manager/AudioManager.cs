using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Sources")]
    public AudioSource bgmSource; // 배경 음악 전용
    public AudioSource sfxPrefab; // SFX용 AudioSource 프리팹

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume;
    [Range(0f, 1f)] public float bgmVolume = 0.4f;
    [Range(0f, 1f)] public float sfxVolume;

    // BGM 클립 리스트
    [Header("BGM Clips")]
    public List<AudioClip> bgmClips; // 선택 가능한 BGM 리스트

    private ObjectPool<AudioSource> sfxPool;
    private int sfxPoolSize = 10; // 필요에 따라 조정

    private Dictionary<string, AudioClip> audioClipCache = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSFXPool()
    {
        if (sfxPrefab == null)
        {
            return;
        }

        sfxPool = new ObjectPool<AudioSource>(sfxPrefab, sfxPoolSize, bgmSource.transform); // BGM Source의 자식으로 설정
    }

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Count)
        {
            return;
        }

        AudioClip bgmClip = GetAudioClip(bgmClips[index].name);

        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume * masterVolume;
        bgmSource.Play();
    }

    public void PlayBGM(string bgmPath)
    {
        AudioClip bgmClip = GetAudioClip(bgmPath);

        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume * masterVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void PlaySFX(string sfxPath)
    {
        AudioClip sfxClip = GetAudioClip(sfxPath);

        AudioSource sfxSource = sfxPool.Get();
        sfxSource.clip = sfxClip;
        sfxSource.volume = sfxVolume * masterVolume;
        sfxSource.Play();

        // SFX 재생이 끝난 후 풀에 반환
        StartCoroutine(ReturnSFXSourceToPool(sfxSource, sfxClip.length));
    }

    private IEnumerator ReturnSFXSourceToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.clip = null;
        sfxPool.ReturnToPool(source);
    }

    private AudioClip GetAudioClip(string path)
    {
        if (audioClipCache.TryGetValue(path, out AudioClip cachedClip))
        {
            return cachedClip;
        }
        else
        {
            AudioClip clip = ResourceManager.Instance.LoadResource<AudioClip>(path);
            if (clip != null)
            {
                audioClipCache[path] = clip;
            }
            return clip;
        }
    }

    // 볼륨 설정 메서드
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        if (bgmSource != null)
        {
            bgmSource.volume = bgmVolume * masterVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        // SFX 볼륨은 재생 시에 설정됨
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        if (bgmSource != null)
        {
            bgmSource.volume = bgmVolume * masterVolume;
        }
    }

 

}
