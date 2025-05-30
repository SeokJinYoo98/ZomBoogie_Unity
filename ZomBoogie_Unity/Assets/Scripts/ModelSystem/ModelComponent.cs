using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ModelComponent : MonoBehaviour
{
    private event Action<bool> OnFlipped;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipX(float mouseX)
    {
        bool isFlip = mouseX < transform.position.x;
        if (isFlip == _spriteRenderer.flipX)
            return;
        _spriteRenderer.flipX = isFlip;
        OnFlipped.Invoke(isFlip);
    }
    public void FlipY(bool flip) => _spriteRenderer.flipY = flip;
    public bool Flip() => _spriteRenderer.flipX;
    public void SubscribeFlipEvent(Action<bool> handler)
    {
        OnFlipped += handler;
    }
    public void UnsubscribeFlipEvent(Action<bool> handler)
    {
        OnFlipped -= handler;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
