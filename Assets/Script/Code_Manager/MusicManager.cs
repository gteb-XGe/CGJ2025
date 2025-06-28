using UnityEngine;
using Manager;
using System.Collections.Generic;
public class MusicManager : _Manager<MusicManager>
{
    [SerializeField] private AudioSource musicSource;  // ����������
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    //��Ƶ·���ļ�Ϊ public const string BGM_MAIN = "Audio/Music/bgm_main";��ʽ

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

    public AudioClip LoadAudio(string path)//������Ƶ
    {
        return (AudioClip)Resources.Load(path);
    }

    public AudioClip GetAudio(string path)//��ȡ��Ƶ������,�����ظ�����
    {
        if (!audioClips.ContainsKey(path))
        {
            audioClips[path] = LoadAudio(path);
        }
        return audioClips[path];
    }

    public void PlayMainMusic(string path, float volume = 1.0f, bool loop = true)//����������
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

    public void PlaySFX(string path, float volume = 1.0f)//playoneshot���Ե��Ӳ���,���ڲ������彻������Ч
    {
        this.musicSource.PlayOneShot(GetAudio(path), volume);
    }


    public AudioSource GetAudioSource(string name, bool loop = true)// ��������ȡָ�����Ƶ�AudioSource
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

    public void PlayAudio(string name, string path, float volume = 1.0f, bool loop = true)// ���Ŷ������Ƶ���Ƶ
    {
        AudioClip clip = GetAudio(path);
        if (clip == null) return;

        AudioSource source = GetAudioSource(name, loop);
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }


    public void StopAudio(string name)// ָֹͣ����Ƶ
    {
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.Stop();
        }
    }

    public void SetAudioVolume(string name, float valume = 0f)//ָ����Ƶ������
    {
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.volume = valume;
        }
    }

    public void RemoveAudioSource(string name)//ɾ������Ҫ����Ƶ
    {
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.Stop(); // ֹͣ����
            Destroy(source.gameObject); // ������Ϸ����
            audioSources.Remove(name); // ���ֵ����Ƴ�
        }
    }

    // �������п��Ƴ�����ƵԴ
    public void CleanupAudioSources(List<string> exceptions = null)//exceptionsΪ��Ҫ����Ƶ
    {
        List<string> sourcesToRemove = new List<string>();

        foreach (var pair in audioSources)
        {
            // ���������б��е���Ƶ
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
