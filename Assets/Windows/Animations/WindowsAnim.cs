using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsAnim : MonoBehaviour
{
    public delegate void ShowWindowDelegate();
    public delegate void HideWindowDelegate();

    public event ShowWindowDelegate ShowWindowEventHandler;
    public event HideWindowDelegate HideWindowEventHandler;

    [SerializeField] private GameObject window;
    private Animator animator;
    private float transitTime = 0.2f;

    void Awake()
    {
        animator = window.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ShowWindowEventHandler += ShowWindow;
        HideWindowEventHandler += HideWindow;
    }

    private void OnDisable()
    {
        ShowWindowEventHandler -= ShowWindow;
        HideWindowEventHandler -= HideWindow;
    }

    //For Test Animation
    private bool isShow = true;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShow = !isShow;
            animator.SetBool("isShow", isShow);
        }
    }
        
    public void ShowWindow()
    {
        window.SetActive(true);
        animator.SetBool("isShow", true);
    }

    public void HideWindow()
    {
        animator.SetBool("isShow", false);
        Invoke("DisableWindow", transitTime);
    }

    private void DisableWindow()
    {
        window.SetActive(false);
    }
}
