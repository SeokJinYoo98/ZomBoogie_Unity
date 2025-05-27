using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AbilitySystem.Data;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Card Components")]
    [SerializeField] private Sprite[]               _cards;
    [SerializeField] private Image                  _iconImage;
    [SerializeField] private TMPro.TextMeshProUGUI  _nameText;
    [SerializeField] private TMPro.TextMeshProUGUI  _descriptionText;
    private Image                                   _cardImage;
    public void Init(AbilityData data)
    {
        _iconImage.sprite       = data.Icon;
        _nameText.text          = data.Name;
        _descriptionText.text   = data.Description;
    }
    private void Awake()
    {
        _cardImage = GetComponent<Image>();
        if (_cardImage == null)
        {
            Destroy( gameObject );
            return;
        }
    }
    void Update()
    {
        if (_iconImage == null) Debug.Log( "Null" );
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardImage.sprite = _cards[1];
        AudioManager.Instance.PlaySfx( "Select" );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardImage.sprite = _cards[0];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.Resume( );
        CardSpawner spawner = GetComponentInParent<CardSpawner>();
        if (spawner != null)
        {
            spawner.DestroyAllCard( );
        }
        else
        {
            Debug.LogWarning( "[BaseCard] CardSpawner를 부모 계층에서 찾을 수 없습니다." );
        }
    }
}
