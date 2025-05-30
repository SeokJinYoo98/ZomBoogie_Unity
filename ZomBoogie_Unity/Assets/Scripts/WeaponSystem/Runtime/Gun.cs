using Common.CharInterface;
using StatSystem.Runtime;
using UnityEngine;
using WeaponSystem.Data;
using WeaponSystem.Module;

public class NewGun : WeaponBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void AttackLogic()
    {
        Debug.Log("GunAttack");
    }
}
