using UnityEngine;
using UnityEngine.EventSystems;

public class WindowsCloseButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private IWindowInterface window;

    private void Start()
    {
        window = GetComponentInParent<IWindowInterface>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        window.HideWindow();
    }

}
