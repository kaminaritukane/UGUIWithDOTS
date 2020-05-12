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
    private Toggle toggle;
    public UnityEvent OnClickEvent;
    public WindowTabEvent OnSelectEvent = new WindowTabEvent();
    public WindowTabEvent OnDeselectEvent = new WindowTabEvent();

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        OnClickEvent?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Select " + eventData.selectedObject.name);
        toggle.isOn = true;
        OnClickEvent?.Invoke();
        OnSelectEvent?.Invoke(this);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Deselect " + eventData.selectedObject.name);
        OnDeselectEvent?.Invoke(this);
    }
}
