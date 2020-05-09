using UnityEngine;
using UnityEngine.EventSystems;

public class WindowsCloseButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private WindowsManager windowsManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        windowsManager.HideWindow();
    }

}
