using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StringEvent : UnityEvent<string>
{

}

public class ScrollViewButton : MonoBehaviour, IPointerClickHandler, ISelectHandler
{
    public StringEvent stringEvent = new StringEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        //stringEvent?.Invoke(gameObject.name);
    }

    public void OnSelect(BaseEventData eventData)
    {
        stringEvent?.Invoke(gameObject.name);
    }
}
