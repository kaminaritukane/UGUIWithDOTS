using UnityEngine;

namespace INF.UI
{
    public class DockingScreen : MonoBehaviour
    {
        [SerializeField] GameObject _shipListContent = null;
        [SerializeField] DockingShipListItem _shipListItemPrefab = null;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void SetupShipList(ShipDataScriptableObject[] shipDatas)
        {
            foreach( var data in shipDatas )
            {
                var item = GameObject.Instantiate(_shipListItemPrefab.gameObject,
                    _shipListContent.transform) as GameObject;
                var itemComp = item.GetComponent<DockingShipListItem>();
                itemComp.Setup(data.shipName, data.shipImage);
            }
        }
    }
}