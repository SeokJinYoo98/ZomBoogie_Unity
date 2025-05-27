using UnityEngine;

namespace AbilitySystem.Data
{
    [CreateAssetMenu( menuName = "MyGame/AbilitySystem/PassiveAbilityData" )]
    public class PassiveAbilityData : AbilityData
    {
        [Header("패시브 수치 설정")]
        [SerializeField] private float _baseValue = 1.0f;       // 기본 증가량
        [SerializeField] private float _valuePerLevel = 0.2f;   // 레벨당 추가 증가량

        // Getter Properties
        public float BaseValue      => _baseValue;
        public float ValuePerLevel  => _valuePerLevel;
    }
}
