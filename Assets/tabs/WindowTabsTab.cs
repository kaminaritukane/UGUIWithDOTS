using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowTabEvent : UnityEvent<WindowTabsTab>
{
}

public class WindowTabsTab : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Text tabNameText = default;
    private Toggle toggle;
    public UnityEvent OnClickEvent;
    public WindowTabEvent OnSelectEvent = new WindowTabEvent();
    public WindowTabEvent OnDeselectEvent = new WindowTabEvent();

    public void Init(string tabName)
    {
        tabNameText.text = tabName;
    }

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        toggle.isOn = true;
        OnClickEvent?.Invoke();
        OnSelectEvent?.Invoke(this);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnDeselectEvent?.Invoke(this);
    }
}
