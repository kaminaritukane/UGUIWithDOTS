using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/ShipDataScriptableObject", order = 1)]
public class ShipDataScriptableObject : ScriptableObject
{
    public string shipName;
    public string description;
    public Sprite shipImage;
    public GameObject shipModel;
}
