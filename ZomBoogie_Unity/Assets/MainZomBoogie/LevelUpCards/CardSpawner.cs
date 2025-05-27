using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using AbilitySystem.Runtime;
public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Object _cardPrefab;

    private UnityAction _onLevelUp;

    private void Start()
    {
        _onLevelUp = OnLevelUp;
        Debug.Log( "이벤트 등록" );
        LevelManager.Instance?.AddLevelUpListener( _onLevelUp );
    }
    private void OnDisable()
    {
        Debug.Log( "이벤트 제거" );
        LevelManager.Instance?.RemoveLevelUpListner( _onLevelUp );
    }

    private void OnLevelUp()
    {
        SpawnCard();
    }
    private void SpawnCard()
    {
        Debug.Log( "카드 스폰" );
        var abilityData = AbilityDataPool.Instance.Get( "ablility_passive_speedup" );
        var cardObj = Instantiate( _cardPrefab, transform );
        var baseCard = cardObj.GetComponent<BaseCard>();
        baseCard.Init( abilityData );

    }
}
