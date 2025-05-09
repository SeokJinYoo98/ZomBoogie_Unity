using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

enum AudioType { BGM, Player, Enemy, UI };

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer         _mixer;
    [SerializeField] private AudioMixerGroup    _bgmGroup;
    [SerializeField] private AudioMixerGroup    _sfxGroup;
    [SerializeField] private AudioMixerGroup    _uiGroup;

    [Header("Volume")]
    [SerializeField] private float _bgm;
    [SerializeField] private float _sfx;
    [SerializeField] private float _ui;

    private Dictionary<string, AudioClip>   _audioClips; 
    private Queue<AudioSource>              _audioSources;

    private void Awake()
    {
        if (Instance)
        {
            Destroy( gameObject );
            return;
        }
        Instance = this;
        DontDestroyOnLoad( gameObject );
        _audioSources = new( );
        LoadMixerGroup( );
        LoadClips();
    }
    void LoadMixerGroup()
    {
        _bgmGroup = _mixer.FindMatchingGroups( "Master/Bgm" )[0];
        _sfxGroup = _mixer.FindMatchingGroups( "Master/Sfx" )[0];
        _uiGroup = _mixer.FindMatchingGroups( "Master/UI" )[0]; 
    }
    void LoadClips()
    {
        _audioClips = new( );

        foreach (var clip in Resources.LoadAll<AudioClip>( "Audio" ))
            _audioClips[clip.name] = clip;

        Debug.Log( $"[Audio] {_audioClips.Count} clips loaded." );
    }
    public void PlaySfx(string name)
    {
        if (!_audioClips.TryGetValue( name, out var clip )) return;

        var src = _audioSources.Count > 0                              
              ? _audioSources.Dequeue()
              : gameObject.AddComponent<AudioSource>();

        src.outputAudioMixerGroup = _sfxGroup;                          
        src.clip = clip;
        src.volume = 1;
        src.Play( );                                                     

        StartCoroutine( ReturnAfter( src, clip.length ) ); 
    }

    IEnumerator ReturnAfter(AudioSource src, float t)
    {
        yield return new WaitForSeconds( t );
        src.clip = null;
        _audioSources.Enqueue( src );
    }
}
