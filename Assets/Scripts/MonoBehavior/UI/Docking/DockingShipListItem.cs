using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace INF.UI
{
    public class DockingShipListItem : MonoBehaviour
    {
        [SerializeField] Text _shipNameText = null;
        [SerializeField] Image _shipImage = null;

        public void Setup(string name, Sprite img)
        {
            _shipNameText.text = name;
            _shipImage.sprite = img;
        }
    }
}