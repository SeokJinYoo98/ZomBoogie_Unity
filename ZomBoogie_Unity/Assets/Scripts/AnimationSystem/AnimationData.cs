using UnityEngine;
using System.Collections.Generic;
namespace Animation.Data
{
    [CreateAssetMenu( fileName = "AnimationData", menuName = "MyGame/AnimationSystem/AnimationData" )]
    public class NewAnimationData : ScriptableObject
    {
        [Header ("애니메이션 이름")]
        [SerializeField] private string _animationName;

        [Header("순서대로 재생할 스프라이트 프레임")]
        [SerializeField] private List<Sprite> _frames;

        [Header("초당 프레임 수")]
        [SerializeField] private float  _frameRate = 1.0f;

        [Tooltip("애니메이션 루프")]
        [SerializeField] private bool _loop = true;

        public string                   AnimationName   => _animationName;
        public IReadOnlyList<Sprite>    Frames          => _frames;
        public float                    FrameRate       => _frameRate;
        public bool                     Loop            => _loop;
    }
}


