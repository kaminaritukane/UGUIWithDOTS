using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/*  
    ①PointerEnter：指针（鼠标、手指等）进入目标（Button、Panel等）范围的瞬间；
        ②PointerExit：指针离开目标范围的瞬间；
        ③PointerDown：指针在目标范围内时：按下的瞬间；
        ④PointerUp：指针在目标范围内时：指针抬起的瞬间：即使指针抬起的瞬间不在目标范围内，也会触发；
        ⑤PointerClick：指针对目标完成一次点击的瞬间：如果指针抬起的瞬间不在目标范围内，便不会触发；在PointerUp之后执行；
        ⑥Drag：指针按住并移动时：如果按下不移动则不触发；即使拖动时指针不在目标范围内也会触发；
        ⑦Drop：（理解为有其他东西被扔到监听对象身上时触发）在指针抬起的瞬间：如果被拖拽目标A此时在另一目标B的范围内，并且目标B有对Drop的监听（目标A不需要，但需要能够Drag），则触发；否则不会触发；在PointerUp之后执行；
        ⑧Scroll：指针在目标范围内时：无论指针是否按下，滚动滚轮时触发；
        ⑨UpdateSelected：指针完成一次PointerClick后，每一帧都会执行一次对应事件（下文的Select只在最开始执行一次）；
        ⑩Select：指针在目标范围内完成第一次点击后，目标变为"Select"状态；
        ⑪Deselect：指针在目标范围外完成第一次点击后，目标变为"Deselect"状态；
        ⑫Move：任意目标处于"Select"状态下时：通过『上』『下』『左』『右』键，或"W", "S", "A", "D" 等键改变了Axis轴的Value时触发（可用Input.GetAxis("AxisName")检测）；
        ⑬InitializePotentialDrag：指针在目标范围内按下时：初始化潜在的可拖动目标，在PointerDown之后，BeginDrag之前触发；
        ⑭BeginDrag：指针在目标范围内按下时：一旦移动便触发；即使拖动时指针不在目标范围内也会触发；
        ⑮EndDrag：指针在移动时抬起的瞬间：即使指针不在目标范围内也会触发；如果触发了Drop则在其之后触发；
        ⑯Submit：目标处于"Select"状态下时：点击"Submit"键(默认为"Enter"键)时触发；
    ⑰Cancel：目标处于"Select"状态下时：点击"Cancel"键(默认为"ESC"键)时触发；
*/

public enum ButtonStates
{
    Normal,
    Selected,
    Disabled,
}

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler, IPointerClickHandler, IDragHandler, IDropHandler, ISelectHandler, IDeselectHandler,
    IMoveHandler,IBeginDragHandler, IEndDragHandler, ISubmitHandler, ICancelHandler
{
    public event EventHandler OnButtonPressed;
    public event EventHandler OnButtonClicked;
    public event EventHandler OnEnterButton; //hover, show tips
    public event EventHandler OnExitButton;

    //public UnityEvent OnButtonHold;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnterButton?.Invoke(this, EventArgs.Empty);
        Debug.Log("Enter Button" + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitButton?.Invoke(this, EventArgs.Empty);
        Debug.Log("Exit Button" + gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //OnButtonHold?.Invoke();
        Debug.Log("Button Pressed" + gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button Left" + gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked?.Invoke(this, EventArgs.Empty);
        Debug.Log("Button Clicked" + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag" + gameObject.name);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop" + gameObject.name);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Select" + gameObject.name);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Deselect" + gameObject.name);
    }

    public void OnMove(AxisEventData eventData)
    {
        Debug.Log("Move" + gameObject.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag" + gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag" + gameObject.name);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("Submit" + gameObject.name);
    }

    public void OnCancel(BaseEventData eventData)
    {
        Debug.Log("Cancel" + gameObject.name);
    }
}
