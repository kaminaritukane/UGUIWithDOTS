using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class EventTriggerHandler : MonoBehaviour
{

    void Start()
    {
        Button button = GetComponent<Button>();
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(OnClick);
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(OnEnter);
        trigger.triggers.Add(entry);
    }

    private void OnClick(BaseEventData arg0)
    {
        Debug.Log("Button Clicked. EventTrigger..");
    }

    private void OnEnter(BaseEventData arg0)
    {
        Debug.Log("Button Entered. EventTrigger..");
    }

}
