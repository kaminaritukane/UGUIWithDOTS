using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarSteps : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    private Scrollbar scrollbar;
    private int cellCount;

    private void Awake()
    {
        cellCount = content.childCount;
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.numberOfSteps = cellCount;
    }
}
