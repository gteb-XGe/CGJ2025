using UnityEngine;
using Manager;
using System.Collections.Generic;
public class MusicManager : _Manager<MusicManager>
{
    [SerializeField] private AudioSource musicSource;  // 背景音乐源
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    //音频路径文件为 public const string BGM_MAIN = "Audio/Music/bgm_main";格式

    private void Awake()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

    }

    void Start()
    {

    }

    void Update()
    {

    }

    public AudioClip LoadAudio(string path)//加载音频
    {
        return (AudioClip)Resources.Load(path);
    }

    public AudioClip GetAudio(string path)//获取音频资源，避免重复加载
    {
        if (!audioClips.ContainsKey(path))
        {
            audioClips[path] = LoadAudio(path);
        }
        return audioClips[path];
    }

    public void PlayMainMusic(string path, float volume = 1.0f, bool loop = true)//播放背景音乐
    {
        AudioClip clip = GetAudio(path);
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMainMusic()
    {
        musicSource.Stop();
    }

    public void SetMainMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void PlaySFX(string path, float volume = 1.0f)//playoneshot可以叠加播放，用于音效交叉混合效果
    {
        this.musicSource.PlayOneShot(GetAudio(path), volume);
    }


    public AudioSource GetAudioSource(string name,bool loop = true)// 动态获取指定名称的AudioSource
    {
        if (!audioSources.TryGetValue(name, out AudioSource source))
        {
            GameObject audioObject = new GameObject($"AudioSource_{name}");
            audioObject.transform.SetParent(transform);
            source = audioObject.AddComponent<AudioSource>();
            source.loop = loop;
            audioSources[name] = source;
        }
        return source;
    }

    public void PlayAudio(string name, string path, float volume = 1.0f, bool loop = true)// 播放独立命名的音频
    {
        AudioClip clip = GetAudio(path);
        if (clip == null) return;

        AudioSource source = GetAudioSource(name, loop);
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }


    public void StopAudio(string name)// 停止指定音频
    {
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.Stop();
        }
    }

    public void SetAudioVolume(string name, float valume = 0f)//设置指定音频音量
    {
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.volume = valume;
        }
    }

    public void RemoveAudioSource(string name)//删除不再需要的音频
    {
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.Stop(); // 停止播放
            Destroy(source.gameObject); // 销毁游戏对象
            audioSources.Remove(name); // 从字典中移除
        }
    }

    // 清理所有非保留音频源
    public void CleanupAudioSources(List<string> exceptions = null)//exceptions为需要保留的音频
    {
        List<string> sourcesToRemove = new List<string>();

        foreach (var pair in audioSources)
        {
            // 保留例外列表中的音频
            if (exceptions != null && exceptions.Contains(pair.Key))
                continue;

            sourcesToRemove.Add(pair.Key);
        }

        foreach (string name in sourcesToRemove)
        {
            RemoveAudioSource(name);
        }
    }
}
