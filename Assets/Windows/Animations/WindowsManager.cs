using UnityEngine;

public class WindowsManager : MonoBehaviour, IWindowInterface
{
    public delegate void ShowWindowDelegate();
    public delegate void HideWindowDelegate();

    public event ShowWindowDelegate ShowWindowEventHandler;
    public event HideWindowDelegate HideWindowEventHandler;

    [SerializeField] private GameObject window;
    private Animator animator;
    [SerializeField] private float transitTime = 0.35f;

    void Awake()
    {
        animator = window.GetComponent<Animator>();
        animator.SetFloat("animSpeed", 1f / transitTime);
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShowWindow();
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
