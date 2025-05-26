using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Components")]
    [SerializeField] private Sprite[]               _cards;
    [SerializeField] private Image                  _iconImage;
    [SerializeField] private TMPro.TextMeshProUGUI  _nameText;
    [SerializeField] private TMPro.TextMeshProUGUI  _descriptionText;
    private Image                                   _cardImage;
    public void Init(IAbility ability)
    {
        _iconImage.sprite       = ability.AbilityIcon;
        _nameText.text          = ability.Name;
        _descriptionText.text   = ability.Description;
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
}
