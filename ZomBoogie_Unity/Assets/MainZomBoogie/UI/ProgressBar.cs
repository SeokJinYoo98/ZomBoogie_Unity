using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum ProgressType { Hp, Xp };
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject         _target;
    [SerializeField] private Scrollbar          _bar;
    [SerializeField] private ProgressType       _progressType;
    [SerializeField] private TextMeshProUGUI    _valueText;

    int     _currValue = 0;
    int     _maxValue = 0;
    void Start()
    {
    }

    void Update()
    {
        switch (_progressType)
        {
            case ProgressType.Hp: UpdateHp( ); break;
            case ProgressType.Xp: UpdateXp( ); break;
        }
    }
    private void UpdateHp()
    {
        if (!_target.TryGetComponent<IDamageable>( out var dmg )) return;
        var (curr, max) = dmg.GetHp( );
        if (curr == _currValue && max == _maxValue) return;

        _currValue = curr;
        _maxValue = max;
        float percent = CalcPercent(curr, max);

        _bar.numberOfSteps = _maxValue;
        _bar.size = percent;

        _valueText.text = $"{curr}";
    }
    private void UpdateXp()
    {
        if (!_target.TryGetComponent<Boogie>( out var bg )) return;
        var (curr, max) = bg.GetXp( );
        if (curr == _currValue && max == _maxValue) return;

        _currValue = curr;
        _maxValue = max;
        float percent = CalcPercent(curr, max);

        _bar.numberOfSteps = _maxValue;
        _bar.size = percent;

        _valueText.text = $"{curr} / {max}";
    }
    private float CalcPercent(int now, int max)
    {
        if (now <= 0 || max <= 0)
            return 0.0f;
        return Mathf.Clamp01( (float)now / max );
    }
}
