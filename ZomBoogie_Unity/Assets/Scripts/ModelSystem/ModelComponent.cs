using UnityEngine;
using Common.Interface;
[RequireComponent( typeof( SpriteRenderer ) )]
public class ModelComponent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public void FlipX(bool flip) => _spriteRenderer.flipX = flip;
    public void FlipY(bool flip) => _spriteRenderer.flipY = flip;
    public bool Flip() => _spriteRenderer.flipX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
