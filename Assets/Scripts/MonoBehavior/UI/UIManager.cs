using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace INF.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private DockingUI _dockingUI = null;

        private void Awake()
        {
            var uiSysGroup = World.DefaultGameObjectInjectionWorld.GetExistingSystem<UISystemGroup>();
            if (uiSysGroup != null)
            {
                var dockingSys = uiSysGroup.DockingUISys;
                dockingSys.OnDockableChangedEvent += OnDockableChangedEvent;
            }
        }

        private void OnDockableChangedEvent(bool dockable)
        {
            _dockingUI.OnDockableChanged(dockable);
        }
    }
}