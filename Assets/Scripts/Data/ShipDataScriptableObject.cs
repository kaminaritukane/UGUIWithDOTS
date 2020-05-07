using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/ShipDataScriptableObject", order = 1)]
public class ShipDataScriptableObject : ScriptableObject
{
    public string shipName;
    public string description;
    public GameObject shipModel;
}
