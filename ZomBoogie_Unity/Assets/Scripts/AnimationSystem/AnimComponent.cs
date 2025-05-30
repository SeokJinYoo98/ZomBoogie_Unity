using Animation.Data;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimComponent : MonoBehaviour
{
    [SerializeField] private NewAnimationData[]     _datas;

    private SpriteRenderer  _spriteRenderer;
    private Rigidbody2D     _rb2d;
    private Dictionary<string, NewAnimationData> _dataMap;

    private NewAnimationData _currAnim;
    private string           _currName;
    private float            _timer;
    private int              _frame;
    private float            _frameTime;
    private bool             _isReverse = false;

    void Awake()
    {
        if (_datas == null || _datas.Length == 0)
            throw new System.Exception("애니메이션 데이터가 없습니다");

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (!_spriteRenderer) throw new System.Exception("sr이 없어요");

        _dataMap = new Dictionary<string, NewAnimationData>();
        foreach (var data in _datas)
        {
            if (data == null) continue;
            string key = data.AnimationName;
            if (string.IsNullOrEmpty(key)) continue;
            if (_dataMap.ContainsKey(key)) continue;
            _dataMap[key] = data;
        }
        EnterAnimation("Walk");
        
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayAnimation(Time.deltaTime);
    }

    public void SetReverse(bool flipX)
    {
        if (flipX)
        {
            if (_rb2d.linearVelocityX <= 0.0f) _isReverse = false;
            else _isReverse = true;
        }
        else
        {
            if (_rb2d.linearVelocityX <= 0.0f) _isReverse = true;
            else _isReverse = false;
        }
    }
    public void EnterAnimation(string name)
    {
        if (_currName == name || !_dataMap.ContainsKey(name))
            return;

        _currName = name;
        _currAnim = _dataMap[name];

        _timer = 0f;
        _frame = 0;
        _frameTime = 1f / Mathf.Max(_currAnim.FrameRate, 0.01f);
        _spriteRenderer.sprite = _currAnim.Frames[0];
    }
    private void PlayAnimation(float deltaTime)
    {
        if (_currAnim == null || _currAnim.Frames == null || _currAnim.Frames.Count <= 1) 
            return;

        _timer += deltaTime;
        if (_timer >= _frameTime)
        {
            _timer -= _frameTime;
            if (_isReverse)
                _frame = (_frame - 1 + _currAnim.Frames.Count) % _currAnim.Frames.Count;
            else
                _frame = (_frame + 1) % _currAnim.Frames.Count;
            _spriteRenderer.sprite = _currAnim.Frames[_frame];
        }
    }
}
