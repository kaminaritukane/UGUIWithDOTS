using UnityEngine;
using UnityEngine.UI;

public class TooltipsManager : Singleton<TooltipsManager>
{
    [SerializeField] private GameObject tooltipsLabel; 
    private RectTransform rectTransform;
    private RectTransform parent;
    private Text text;

    private void Start()
    {
        rectTransform = tooltipsLabel.GetComponent<RectTransform>();
        text = tooltipsLabel.GetComponent<Text>();
    }

    public void ShowTooltips(RectTransform parent, TooltipAnchor tooltipAnchor, string content)
    {
        this.parent = parent;
        Vector2 pos = parent.anchoredPosition;
        Vector2 size = parent.sizeDelta;
        Vector2 labelSize = rectTransform.sizeDelta;

        switch(tooltipAnchor)
        {
            case TooltipAnchor.Above:
                text.alignment = TextAnchor.MiddleCenter;
                rectTransform.anchoredPosition = pos + new Vector2(0, size.y / 2 + labelSize.y / 2); 
                break;
            case TooltipAnchor.Below:
                text.alignment = TextAnchor.MiddleCenter;
                rectTransform.anchoredPosition = pos - new Vector2(0, size.y / 2 + labelSize.y / 2); 
                break;
            case TooltipAnchor.Left:
                text.alignment = TextAnchor.MiddleRight;
                rectTransform.anchoredPosition = pos - new Vector2(size.x /2 + labelSize.x /2, 0); 
                break;
            case TooltipAnchor.Right:
                text.alignment = TextAnchor.MiddleLeft;
                rectTransform.anchoredPosition = pos + new Vector2(size.x / 2 + labelSize.x / 2, 0); 
                break;
            default: Debug.LogError("TooltipAnchor was not in the list!"); break;
        }

        text.text = content;
        tooltipsLabel.SetActive(true);
    }

    public void HideTooltips(RectTransform parent)
    {
        if(this.parent == parent)
        {
            tooltipsLabel.SetActive(false);
        }
    }
}

