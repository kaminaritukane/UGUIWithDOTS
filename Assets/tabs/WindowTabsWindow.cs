using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTabsWindow : MonoBehaviour
{
    private WindowTabsAutoManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<WindowTabsAutoManager>();
    }

    private void Start()
    {
        manager.AddWindow(gameObject);
    }
}
