using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SoundManager
{
    public AudioSource[] AudioSources { get; private set; } = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public AudioSource GetAudio(Define.Sound soundType) { return AudioSources[(int)soundType]; }

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for(int i = 0; i<soundNames.Length-1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                AudioSources[i] = go.AddComponent<AudioSource>();

                go.transform.parent = root.transform;
                    
            }
            AudioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in AudioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.SE,float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.SE, float pitch = 1.0f)
    {

        if (type == Define.Sound.Bgm)
        {

            AudioSource audioSource = AudioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying == true)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = AudioSources[(int)Define.Sound.SE];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);

        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.SE)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);

        }
        else
        {
            //_audioClips에 해당 벨류값이 없을때 = 처음 호출됐을때, 새 오디오 클립 저장
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"Audio Clip Missing! {path}");

        return audioClip;

    }

    public void SetVolume(float volumeValue, Define.Sound volume)
    {
        AudioSources[(int)volume].volume = volumeValue;
        Managers.Data.SaveVolumeSetting(volume);
    }
}
