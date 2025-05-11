using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    private Dictionary<string, AudioClip>   _sfxClips;
    private Dictionary<string, SfxMeta>     _sfxMeta = new( );

    private Dictionary<string, AudioClip>   _bgmClips;
    private Dictionary<string, BgmMeta>     _bgmMeta = new( );

    private Dictionary<string, AudioClip>   _uiClips;
    private Dictionary<string, UiMeta>      _uiMeta  = new( );

    private Queue<AudioSource>              _audioSources = new( );
    private void Awake()
    {
        if (Instance)
        {
            Destroy( gameObject );
            return;
        }
        Instance = this;
        DontDestroyOnLoad( gameObject );
  
        LoadSfxMeta();
        LoadClips();
    }
    void LoadSfxMeta()
    {
        var ta = Resources.Load<TextAsset>("Audio/Sfx/SfxData");
        if (ta == null)
        {
            Debug.LogError( "SfxData.json not found in Resources/Audio/Sfx/" );
            return;
        }
        _sfxMeta = JsonConvert
            .DeserializeObject<Dictionary<string, SfxMeta>>( ta.text );
    }
    void LoadClips()
    {
        _sfxClips = new( );
        foreach (var clip in Resources.LoadAll<AudioClip>( "Audio/Sfx" ))
            _sfxClips[clip.name] = clip;

        _bgmClips = new( );
        foreach (var clip in Resources.LoadAll<AudioClip>( "Audio/Bgm" )) 
           _bgmClips[clip.name] = clip;

        _uiClips = new( );
        foreach (var clip in Resources.LoadAll<AudioClip>( "Audio/Ui" ))
            _uiClips[clip.name] = clip;
    }
    public void PlaySfx(string name)
    {
        // 1) 클립 가져오기
        if (!_sfxClips.TryGetValue( name, out var clip ))
        {
            Debug.LogWarning( $"PlaySfx: Clip not found for ID '{name}'" );
            return;
        }

        // 2) 메타 가져오기 (없으면 기본값)
        if (!_sfxMeta.TryGetValue( name, out var meta ))
            meta = new SfxMeta( );  // volume=1, pitchMin=1, pitchMax=1

        // 3) AudioSource 풀에서 꺼내거나 새로 생성
        AudioSource src = _audioSources.Count > 0
        ? _audioSources.Dequeue()
        : gameObject.AddComponent<AudioSource>();

        // 4) 파라미터 세팅
        src.outputAudioMixerGroup   = _sfxGroup;                       // SFX 믹서 그룹
        src.clip                    = clip;
        src.volume                  = meta.volume * _sfx;
        src.pitch                   = Random.Range( meta.pitchMin, meta.pitchMax );
       
        // 5) 재생
        src.Play( );

        // 6) 끝나면 풀에 반납
        StartCoroutine( ReturnAfter( src, clip.length ) );
    }

    IEnumerator ReturnAfter(AudioSource src, float t)
    {
        yield return new WaitForSeconds( t );
        src.clip = null;
        _audioSources.Enqueue( src );
    }
}
