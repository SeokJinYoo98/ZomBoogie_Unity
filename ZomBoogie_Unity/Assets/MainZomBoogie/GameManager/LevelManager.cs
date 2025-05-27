using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI    _timerText;
    [SerializeField] private GameObject         _panel;
    [SerializeField] private LayerMask          _gameplayLayer;
    private const float                         _startTime = 600.0f;
    private float                               _remainingTime;


    private UnityEvent _onLevelUp;

    private void Awake()
    {
        if (Instance)
        {
            Destroy( gameObject );
            return;
        }
        Instance = this;
        _panel.SetActive(false);
        _remainingTime = _startTime;
        _onLevelUp = new UnityEvent( );
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
    }
    void UpdateTimerUI()
    {
        if (_remainingTime <= 0f) return;

        _remainingTime -= Time.deltaTime;
        if (_remainingTime < 0f) _remainingTime = 0f;

        int minutes = Mathf.FloorToInt(_remainingTime / 60.0f);
        int seconds = Mathf.FloorToInt(_remainingTime % 60.0f);
        _timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
    public void PlayerLevelUp()
    {
        PauseGame( );
        _onLevelUp.Invoke();
    }
    public void PauseGame()
    {
        AudioManager.Instance.PlaySfx( "LevelUp" );
        _panel.SetActive( true );
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        _panel.SetActive( false );
        Time.timeScale = 1.0f;
    }

    public void AddLevelUpListener(UnityAction action) 
        => _onLevelUp.AddListener(action);
    public void RemoveLevelUpListner(UnityAction action)
        => _onLevelUp.RemoveListener(action);
}
