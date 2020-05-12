using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTabsWindow : MonoBehaviour
{
    [SerializeField] private string windowName = default;

    public string WindowName
    {
        get => windowName;
    }
}
