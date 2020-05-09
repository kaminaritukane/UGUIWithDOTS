using UnityEngine;
using UnityEngine.EventSystems;

public enum TooltipAnchor
{
    Above,
    Below,
    Left, //The tooltip will show in the left of the object, so pos x is minus, and text alignment is MiddleRight
    Right   
}

public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltip;
    [SerializeField] private TooltipAnchor tooltipAnchor;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipsManager.Instance.ShowTooltips(rectTransform, tooltipAnchor, tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipsManager.Instance.HideTooltips(rectTransform);
    }
}
