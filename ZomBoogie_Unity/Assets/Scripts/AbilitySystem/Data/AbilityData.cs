using UnityEngine;
using AbilitySystem.Core;

namespace AbilitySystem.Data
{
    public class AbilityData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string         _id;
        [SerializeField] private string         _name;
        [SerializeField] private string         _description;
        [SerializeField] private AbilityType    _type;

        [Header("아이콘 및 시각 자산")]
        [SerializeField] Sprite                 _icon;

        // Getter Properties
        public string           Id           => _id;
        public string           Name         => _name;
        public string           Description  => _description;
        public AbilityType      Type         => _type;
        public Sprite           Icon         => _icon;
    }


}
