using UnityEngine;

[CreateAssetMenu(fileName = "AudioAsset", menuName = "Audio/AudioAsset" )]
public abstract class AudioAsset : ScriptableObject
{
    public AudioClip Clip;
    [Range(0, 1)] public float Volume = 1.0f;
    
    public abstract void Play(float volume);
    public abstract void Stop();
}

[CreateAssetMenu(fileName = "SfxAudioAsset", menuName = "Audio/SFX")]
public class SfxAudioAsset : AudioAsset
{
    public float PitchMin = 1.0f, PitchMax = 1.0f;
    public SfxAudioAsset(AudioClip clip)
    {

    }
    override public void Play(float volume)
    {

    }
    override public void Stop()
    {

    }
}
[CreateAssetMenu( fileName = "BgmAudioAsset", menuName = "Audio/BGM" )]
public class BgmAudioAsset : AudioAsset
{
    override public void Play(float volume)
    {

    }
    override public void Stop()
    {

    }
}
[CreateAssetMenu( fileName = "UIAudioAsset", menuName = "Audio/UI" )]
public class UIAudioAsset : AudioAsset
{
    override public void Play(float volume)
    {

    }
    override public void Stop()
    {

    }
}
