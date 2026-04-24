using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private bool isCard;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isCard)
            soundManager.PlayCardHover();
        else
            soundManager.PlayButtonHover();
    }
}