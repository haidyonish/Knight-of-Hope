using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoverVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image targetImage;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(0.75f, 0.75f, 0.75f, 1f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetImage.color = normalColor;
    }
}