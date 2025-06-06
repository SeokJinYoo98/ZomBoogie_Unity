using StatSystem.Core;
using UnityEngine;
namespace StatSystem.Data
{
    [CreateAssetMenu( menuName = "MyGame/StatSystem/StatData" )]
    public class StatData : ScriptableObject
    {
        [Header("스탯 정보")]
        [SerializeField] private string     _id;
        [SerializeField] private string     _statName;
        [SerializeField] private StatType   _statType;
       
        [Header("기본 값")]
        [SerializeField] private float _baseValue = 100f;
        [SerializeField] private bool   _zeroStart;

        public StatType StatType => _statType;
        public float BaseValue => _baseValue;
        public string Id            => _id;
        public string Name          => _statName;
        public bool   IsZeroStart => _zeroStart;
    }
}

