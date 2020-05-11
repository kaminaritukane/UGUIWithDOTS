using UnityEngine;

namespace INF.UI
{
    public class DockingUI : MonoBehaviour
    {
        [SerializeField] DockingButton _dockingButton = null;
        [SerializeField] DockingScreen _dockingScreen = null;

        [SerializeField] ShipDataScriptableObject[] _shipDatas = null;

        private bool isInDockingScreen = false;

        private void Start()
        {
            _dockingButton.Close();

            _dockingScreen.SetupShipList(_shipDatas);
            _dockingScreen.Close();
        }

        public void OnDockableChanged(bool dockable)
        {
            if (dockable)
            {
                _dockingButton.Open();
                _dockingScreen.Close();
            }
            else
            {
                _dockingButton.Close();
                _dockingScreen.Close();
            }

            isInDockingScreen = false;
        }

        public void OnDockingButtonClick()
        {
            _dockingButton.Close();
            _dockingScreen.Open();
            isInDockingScreen = true;
        }

        public void OnCancelButtonClick()
        {
            _dockingButton.Open();
            _dockingScreen.Close();
            isInDockingScreen = false;
        }

        public void OnConfirmButtonClick()
        {

        }
    }
}