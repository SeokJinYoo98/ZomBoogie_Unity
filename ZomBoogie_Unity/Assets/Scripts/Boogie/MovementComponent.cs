using StatSystem.Runtime;
using StatSystem.Core;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    const string _speedId = "stat_base_speed";
    private IStat _stat;
    private Rigidbody2D _rb;
    void Start()
    {
        _stat = GetComponent<StatsComponent>().GetStat(_speedId);
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetMovement(Vector2 dir)
    {
        _rb.linearVelocity = dir * _stat.Value;
    }

}