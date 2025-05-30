using System;
using UnityEngine;
using StatSystem.Core;
using StatSystem.Data;



namespace StatSystem.Module
{
    // 인터렉터블, 논인터렉터블로 나누자.
    public class StatBase : IStat
    {
        public event Action<StatChangeType, float, float> OnValueChanged = delegate { };
        readonly private StatData _data;

        private float           _value;
        private float           _maxValue;
        readonly private float  _baseValue;
        public StatType Type            => _data.StatType;
        public string   Name            => _data.Name;
        public string   Id              => _data.Id;
        public float    BaseValue       => _baseValue;
        public float    MaxValue        => _maxValue;
        public float    Value           => _value;

        public StatBase(StatData data)
        {
            _data  = data;
            _value = _baseValue = _maxValue = data.BaseValue;
            if (_data.IsZeroStart) _value = 0.0f;
        }
        public void SetCurrentStat(float value)
        {
            float newValue = Mathf.Clamp(value, 0f, _maxValue);
            if (Mathf.Approximately( newValue, _value )) return;
            _value = newValue;
            OnValueChanged?.Invoke( StatChangeType.CurrentValue, _value, _maxValue );
        }
        public void IncreaseStat(float amount)   => SetCurrentStat( _value + amount );
        public void DecreaseStat(float amount)   => SetCurrentStat( _value - amount );
        public void ResetStat()                  => SetCurrentStat( _maxValue );
        public void AddAdditionalStat(float amount)
        {
            _maxValue += amount;
            OnValueChanged?.Invoke( StatChangeType.MaxValue, _value, MaxValue );
        }
        public void RemoveAdditionalStat(float amount)
        {
            _maxValue -= amount;
            // current 가 max 초과 시 클램프
            if (_value > _maxValue)
                _value = _maxValue;
            OnValueChanged?.Invoke( StatChangeType.MaxValue, _value, MaxValue );
        }
    }
}
