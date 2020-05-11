using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField] GameObject content;

    void Start()
    {
        ScrollViewButton[] scrollViewButtons = content.GetComponentsInChildren<ScrollViewButton>();
        for (int i = 0; i < scrollViewButtons.Length; i++)
        {
            scrollViewButtons[i].stringEvent.AddListener(ButtonHandler);
        }
    }

    void ButtonHandler(string buttonName)
    {
        //Change model to that one.
        Debug.Log(buttonName);
    }
}
