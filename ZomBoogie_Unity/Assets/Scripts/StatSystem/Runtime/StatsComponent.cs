using UnityEngine;
using System.Collections.Generic;
using StatSystem.Core;
using StatSystem.Data;
using StatSystem.Module;
using System.Linq;

namespace StatSystem.Runtime
{
    public class StatsComponent : MonoBehaviour
    {
        [Header("SO 스탯을 만들어 넣어주세요.")]
        [SerializeField] private StatData[] _statDatas;


        private Dictionary<string, IStat> _stats;

        private void Awake()
        {
            _stats = _statDatas
                .Select(d => (IStat)new StatBase(d))
                .ToDictionary(s => s.Id, s => s);
        }

        public IStat GetStat(string id) => _stats.TryGetValue(id, out var stat) ? stat : null;
        public float GetStatValue(string id) => _stats.TryGetValue(id, out var stat) ? stat.Value : 0f;
        public bool IsDeath() => _stats["stat_base_hp"].Value <= 0.0f;
    }
}

