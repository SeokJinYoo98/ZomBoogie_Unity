using UnityEngine;
using UnityEngine.UI;
using TMPro;

using StatSystem.Core;
using StatSystem.Runtime;
enum ProgressType { Hp, Xp };
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private string             _statId;
    [SerializeField] private Scrollbar          _bar;
    [SerializeField] private TextMeshProUGUI    _valueText;

    private bool    _animate        = false;
    private float   _currPercent    = 0.0f;
    private float   _newPercent     = 0.0f;
    private float   _animationSpeed = 0.5f;
    void Start()
    {
        var boogie = FindFirstObjectByType<Boogie>();
        var stat = boogie.GetComponent<StatsComponent>( ).GetStat( _statId );

        stat.OnValueChanged += UpdateUI;
        UpdateUI( 0, stat.Value, stat.MaxValue );
    }
    private void Update()
    {
        if (_animate) BarAnimation( );
    }
    private void UpdateUI(StatChangeType type, float current, float max)
    {
        Debug.Log( "이벤트 발생" );
        Debug.Log( "Current: " + current + ", Max: " + max );
        _newPercent = Mathf.Clamp01( current / max );
        _bar.numberOfSteps  = (int)max;
        _animate = true;
    }
    private void BarAnimation()
    {
        _currPercent = Mathf.MoveTowards( _currPercent, _newPercent, _animationSpeed * Time.deltaTime );

        if (Mathf.Approximately( _currPercent, _newPercent ))
            _animate = false;

        _bar.size = _currPercent;
    }
}
