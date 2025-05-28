using UnityEngine;
using System.Collections.Generic;
using AbilitySystem.Data;

namespace AbilitySystem.Runtime
{
    public class AbilityDataPool : MonoBehaviour
    {
        public static AbilityDataPool Instance { get; private set; }
        [Header("어빌리티 세팅")]
        [SerializeField] private List<PassiveAbilityData> _passiveDatas;
        // [SerializeField] private List<ActiveAbilityData> _activateDatas;
        // [SerializeField] private List<DebuffAbilityData> _debuffDatas;

        private Dictionary<string, AbilityData> _datas;

        void Awake()
        {
            if (Instance)
            {
                Destroy( gameObject );
                return;
            }
            Instance = this;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _datas = new Dictionary<string, AbilityData>( );

            foreach (var data in _passiveDatas)
                _datas[data.Id] = data;
        }

        public AbilityData Get(string id)
        {
            return _datas.TryGetValue( id, out var data ) ? data : null;
        }
        //public AbilityData GetRandomAbility()
        //{
        //    return _datas.TryGetValue( id, out var data ) ? data : null;
        //}
    }

}
