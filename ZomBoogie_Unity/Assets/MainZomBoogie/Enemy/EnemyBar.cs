using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBar : MonoBehaviour
{
    [SerializeField] private Scrollbar _hpScrollbar;
    [SerializeField] private BaseEnemy _base;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _base = GetComponent<BaseEnemy>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHp();
    }
    private void UpdateHp()
    {
    }
    private float CalcPercent(int now, int max)
    {
        if (now <= 0 || max <= 0)
            return 0.0f;
        return Mathf.Clamp01( (float)now / max );
    }
}
