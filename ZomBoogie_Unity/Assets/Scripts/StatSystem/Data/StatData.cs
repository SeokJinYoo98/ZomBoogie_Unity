using UnityEngine;
namespace StatSystem.Data
{
    [CreateAssetMenu( menuName = "MyGame/StatSystem/StatData" )]
    public class StatData : ScriptableObject
    {
        [Header("Optional Metadata")]
        [SerializeField] private string _id;
        [SerializeField] private string _statName;
       
        [Header("Base / Max Values")]
        [SerializeField] private float _baseValue = 100f;
        [SerializeField] private bool   _zeroStart;
        public float BaseValue      => _baseValue;
        public string Id            => _id;
        public string Name          => _statName;
        public bool   IsZeroStart => _zeroStart;
    }
}

