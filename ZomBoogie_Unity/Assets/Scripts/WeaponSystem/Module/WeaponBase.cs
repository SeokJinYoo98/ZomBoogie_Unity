using UnityEngine;
using System.Collections.Generic;
using StatSystem.Core;
using WeaponSystem.Core;
using WeaponSystem.Data;
using StatSystem.Runtime;


namespace WeaponSystem.Module
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponBase : MonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Vector3 _offset = Vector3.zero;
        private SpriteRenderer _sr;
        public string Id => _weaponData.Id;
        public string Name => _weaponData.Name;
        public Sprite Icon => _weaponData.Icon;

        protected float _cooldownTime = -1.0f;
        readonly protected Dictionary<string, IStat> _attackStats = new();

        void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if (!CanAttack())
                _cooldownTime -= Time.deltaTime;
        }
        public bool CanAttack()
        {
            return _cooldownTime <= 0f;
        }

        public void Attack()
        {
            AttackLogic();
            _cooldownTime = _attackStats[AttackStatKeys.CoolDown].Value;
        }

        public void InitBoogieStats(StatsComponent stats)
        {
            _attackStats.Clear();

            _attackStats[AttackStatKeys.CoolDown]    = stats.GetStat(AttackStatKeys.CoolDown);
            _attackStats[AttackStatKeys.Range]       = stats.GetStat(AttackStatKeys.Range);
            _attackStats[AttackStatKeys.Rate]        = stats.GetStat(AttackStatKeys.Rate);
            _attackStats[AttackStatKeys.Speed]       = stats.GetStat(AttackStatKeys.Speed);
            _attackStats[AttackStatKeys.Penetration] = stats.GetStat(AttackStatKeys.Penetration);
            _attackStats[AttackStatKeys.RowCount]    = stats.GetStat(AttackStatKeys.RowCount);
            _attackStats[AttackStatKeys.ColCount]    = stats.GetStat(AttackStatKeys.ColCount);

            _cooldownTime = _attackStats[AttackStatKeys.CoolDown].Value;
        }

        public void FlipX(bool flipX)
        {
            transform.position += flipX ? _offset : -_offset;
            _sr.flipX = flipX;
        }

        public void LookAt(Vector2 targetPos)
        {
            Vector2 pos = transform.position;
            Vector2 dir = targetPos - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (_sr.flipX) angle += 180f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        protected virtual void AttackLogic()
        {
            Debug.Log("Fire");
        }
    }
}


