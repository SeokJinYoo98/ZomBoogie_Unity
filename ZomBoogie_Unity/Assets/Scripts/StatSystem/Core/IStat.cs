using System;
public enum StatChangeType { CurrentValue, MaxValue }
namespace StatSystem.Core
{
    public interface IStat
    {
        // id, eventType, currValue, maxValue
        event Action< StatChangeType, float, float> OnValueChanged;
        string Id { get; }
        string Name { get; }
        float Value         { get; }
        float BaseValue     { get; }
        float MaxValue      { get; }
        void SetCurrentStat(float value);
        void IncreaseStat(float amount);
        void DecreaseStat(float amount);
        void AddAdditionalStat(float amount);
        void RemoveAdditionalStat(float amount);
        void ResetStat();
    }
}

