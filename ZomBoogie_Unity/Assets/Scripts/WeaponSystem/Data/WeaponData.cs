using UnityEngine;
using StatSystem.Data;
namespace WeaponSystem.Data
{
    [CreateAssetMenu(menuName = "MyGame/WeaponSystem/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Header("웨폰 정보")]
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _sfxName;
        [SerializeField] private float _effectName;
        public Sprite Icon => _sprite;
        public string Id => _id;
        public string Name => _name;
        public string SfxName => _sfxName;
        public float  EffectName => _effectName;
    }
}
