using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowTabsAutoManager : MonoBehaviour, IWindowInterface
{
    [SerializeField] private GameObject tabPrefab;
    [SerializeField] private Transform tabList;
    private ToggleGroup toggleGroup;

    private Dictionary<GameObject, GameObject> windowTabDic = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        toggleGroup = tabList.GetComponent<ToggleGroup>();

        int windowCount = transform.childCount;

        //0 is tabList, 1 is close button
        for(int i = 2; i < windowCount; i++)
        {
            AddWindow(transform.GetChild(i).gameObject);
        }

        EventSystem.current.SetSelectedGameObject(windowTabDic.Keys.First());
    }

    //private void OnEnable()
    //{
    //    if (EventSystem.current == null || EventSystem.current.alreadySelecting)
    //        return;
    //    EventSystem.current.SetSelectedGameObject(windowTabDic.Keys.First());
    //}

    public void AddWindow(GameObject window)
    {
        GameObject tabObj = Instantiate(tabPrefab, tabList);

        tabObj.GetComponent<Toggle>().group = toggleGroup;
        var tab = tabObj.GetComponent<WindowTabsTab>();
        tab.OnClickEvent.AddListener(() => OpenWindow(tabObj));

        windowTabDic.Add(tabObj, window);
    }

    public void RemoveWindow(GameObject window)
    {
        GameObject _key;
        foreach(var kvp in windowTabDic)
        {
            if(kvp.Value == window)
            {
                _key = kvp.Key;
                windowTabDic.Remove(_key);
                return;
            }
        }
    }

    public void OpenWindow(GameObject tab)
    {
        foreach(var kvp in windowTabDic)
        {
            if(kvp.Key == tab)
            {                
                kvp.Value.SetActive(true);
            }
            else
            {
                kvp.Value.SetActive(false);
            }
        }
    }

    public void ShowWindow()
    {

    }

    public void HideWindow()
    {
        gameObject.SetActive(false);
    }    
}
