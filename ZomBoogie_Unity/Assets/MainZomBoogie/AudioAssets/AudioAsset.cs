using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
// 1) JsonUtility 용 래퍼 타입  
[System.Serializable]
public class Pair<T>
{
    public string key;
    public T      value;
}

[System.Serializable]
public class Wrapper<T>
{
    public Pair<T>[] items;
}

public class SfxMeta { public float volume=1f; public float pitchMin=1f; public float pitchMax=1f; }
[System.Serializable]
public class BgmMeta { public float volume=1f; /* 필요에 따라 페이드 시간 등 추가 */ }
[System.Serializable]
public class UiMeta { public float volume=1f; /* UI 쿨타임 등 추가 */ }